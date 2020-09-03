using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Globalization;

namespace PokeRed_BaseStat_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string PokeRedFolder = "";

        private const string POKESTAT_FOLDER = "\\data\\pokemon\\base_stats\\";
        private const string POKEPIC_FOLDER = "\\gfx\\pokemon\\";
        private const string POKEPIC_FRONT_FOLDER = "\\gfx\\pokemon\\front\\";

        List<PokeStat> pokeStats = new List<PokeStat>();

        PokeStat currentlySelected = null;

        public MainWindow()
        {
            InitializeComponent();

            bool goodToGo = false;

            do
            {
                MessageBox.Show("Please select the Pokemon Red Repository Folder");

                CommonOpenFileDialog cofd = new CommonOpenFileDialog();
                cofd.InitialDirectory = "C:\\";
                cofd.IsFolderPicker = true;

                cofd.Title = "Select Pokemon Red Repository Folder";

                if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    PokeRedFolder = cofd.FileName;

                    if (!Directory.Exists(PokeRedFolder))
                    {
                        MessageBox.Show("This folder does not exist!");
                        continue;
                    }

                    if (!Directory.Exists(PokeRedFolder + POKESTAT_FOLDER))
                    {
                        MessageBox.Show("Unable to find: " + PokeRedFolder + POKESTAT_FOLDER);
                        continue;
                    }

                    if (!Directory.Exists(PokeRedFolder + POKEPIC_FOLDER))
                    {
                        MessageBox.Show("Unable to find: " + PokeRedFolder + POKEPIC_FOLDER);
                        continue;
                    }

                    if (!Directory.Exists(PokeRedFolder + POKEPIC_FRONT_FOLDER))
                    {
                        MessageBox.Show("Unable to find: " + PokeRedFolder + POKEPIC_FRONT_FOLDER);
                        continue;
                    }

                    goodToGo = true;
                }
                else
                {
                    goodToGo = true;
                    this.Close();
                    return;
                }
            }
            while (!goodToGo);

            LoadPokemonNames();

            foreach (PokeType o in Enum.GetValues(typeof(PokeType)))
            {
                Type1Selection.Items.Add(o);
                Type2Selection.Items.Add(o);
            }

            Type1Selection.SelectedItem = PokeType.NORMAL;
            Type2Selection.SelectedItem = PokeType.NORMAL;

            foreach (Move m in Enum.GetValues(typeof(Move)))
            {
                Move1Selection.Items.Add(m);
                Move2Selection.Items.Add(m);
                Move3Selection.Items.Add(m);
                Move4Selection.Items.Add(m);
            }

            Move1Selection.SelectedItem = Move.NO_MOVE;
            Move2Selection.SelectedItem = Move.NO_MOVE;
            Move3Selection.SelectedItem = Move.NO_MOVE;
            Move4Selection.SelectedItem = Move.NO_MOVE;

            foreach (GrowthRate g in Enum.GetValues(typeof(GrowthRate)))
                GrowthRateSelection.Items.Add(g);

            GrowthRateSelection.SelectedItem = GrowthRate.GROWTH_FAST;

            foreach (TMHM tmhm in Enum.GetValues(typeof(TMHM)))
                tmhmSelector.Items.Add(tmhm);

            tmhmSelector.SelectedItem = TMHM.UNUSED;

            pokemonListbox.SelectedIndex = 0;
        }

        void LoadPokemonNames()
        {
            string[] files = Directory.GetFiles(PokeRedFolder + POKESTAT_FOLDER);


            foreach(string name in files)
            {
                PokeStat ps = new PokeStat(name);
                pokeStats.Add(ps);

                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = ps;

                pokemonListbox.Items.Add(lbi);
            }
        }

        private void PokemonListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;

            PokeStat selected = (PokeStat)((ListBoxItem)lb.SelectedItem).Content;

            if(currentlySelected != null)
                SaveChangesToPokeStat(currentlySelected);

            SelectPokemon(selected);

            currentlySelected = selected;
        }

        void SaveChangesToPokeStat(PokeStat stat)
        {
            //Save dexid
            stat.pokedexID = pokeid.Text;

            //Save base stats
            int.TryParse(hpedit.Text, out stat.hp);
            int.TryParse(atkedit.Text, out stat.atk);
            int.TryParse(defedit.Text, out stat.def);
            int.TryParse(spdedit.Text, out stat.spd);
            int.TryParse(spcedit.Text, out stat.spc);

            //Save Type
            stat.type1 = (PokeType)Type1Selection.SelectedItem;
            stat.type2 = (PokeType)Type2Selection.SelectedItem;

            //Catch Rate
            int.TryParse(catchRateEdit.Text, out stat.catch_rate);

            //Base exp;
            int.TryParse(baseExpEdit.Text, out stat.baseExp);

            //Sprite locations
            stat.spriteFrontName = spriteFrontEdit.Text;
            stat.spriteBackName = spriteBackEdit.Text;

            //Base Moves
            stat.baseMoves[0] = (Move)Move1Selection.SelectedItem;
            stat.baseMoves[1] = (Move)Move2Selection.SelectedItem;
            stat.baseMoves[2] = (Move)Move3Selection.SelectedItem;
            stat.baseMoves[3] = (Move)Move4Selection.SelectedItem;

            //Growth Rate
            stat.growthRate = (GrowthRate)GrowthRateSelection.SelectedItem;

            //Sprite dimensions
            int.TryParse(spriteDimX.Text, out stat.spriteDimensionX);
            int.TryParse(spriteDimY.Text, out stat.spriteDimensionY);
        }

        void SelectPokemon(PokeStat stat)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            //Load png
            Uri pictureLocation = new Uri(PokeRedFolder + "//" + stat.picLocation.Replace(".pic", ".png"));
            pokemonPicture.Source = new BitmapImage(pictureLocation);

            //Load name
            pokemonName.Content = stat.name;

            //Load dexid
            pokeid.Text = stat.pokedexID;

            //Load base stats
            hpedit.Text = stat.hp.ToString();
            atkedit.Text = stat.atk.ToString();
            defedit.Text = stat.def.ToString();
            spdedit.Text = stat.spd.ToString();
            spcedit.Text = stat.spc.ToString();

            //Load Type
            Type1Selection.SelectedItem = stat.type1;
            Type2Selection.SelectedItem = stat.type2;

            //Catch Rate
            catchRateEdit.Text = stat.catch_rate.ToString();

            //Base exp;
            baseExpEdit.Text = stat.baseExp.ToString();

            //Sprite locations
            spriteFrontEdit.Text = stat.spriteFrontName;
            spriteBackEdit.Text = stat.spriteBackName;

            //Base Moves
            Move1Selection.SelectedItem = stat.baseMoves[0];
            Move2Selection.SelectedItem = stat.baseMoves[1];
            Move3Selection.SelectedItem = stat.baseMoves[2];
            Move4Selection.SelectedItem = stat.baseMoves[3];

            //Growth Rate
            GrowthRateSelection.SelectedItem = stat.growthRate;

            //Sprite dimensions
            spriteDimX.Text = stat.spriteDimensionX.ToString();
            spriteDimY.Text = stat.spriteDimensionY.ToString();

            testgrid.ItemsSource = stat.tmhms;
        }

        private void TMHM_Remove_Click(object sender, RoutedEventArgs e)
        {
            PokeStat ps = (PokeStat)((ListBoxItem)pokemonListbox.SelectedItem).Content;

            ps.tmhms.Remove((TMHMClass)testgrid.SelectedItem);
        }

        private void TmhmAddButton_Click(object sender, RoutedEventArgs e)
        {
            PokeStat ps = (PokeStat)((ListBoxItem)pokemonListbox.SelectedItem).Content;

            TMHM toAdd = (TMHM)tmhmSelector.SelectedItem;
            bool containsThisAlready = false;

            foreach (TMHMClass t in ps.tmhms)
                if (t.TMHM == Enum.GetName(typeof(TMHM), toAdd))
                    containsThisAlready = true;

            if(!containsThisAlready)
                ps.tmhms.Add(new TMHMClass(toAdd));
        }
        
        private void SaveChangeButton_Click(object sender, RoutedEventArgs e)
        {
            PokeStat selected = (PokeStat)((ListBoxItem)pokemonListbox.SelectedItem).Content;

            if (selected != null)
                SaveChangesToPokeStat(currentlySelected);

            string[] files = Directory.GetFiles(PokeRedFolder + POKESTAT_FOLDER);

            string newDirectory = PokeRedFolder + POKESTAT_FOLDER + "Backup - " + DateTime.Now.ToLongDateString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();

            Directory.CreateDirectory(newDirectory);

            foreach (string s in files)
                File.Move(s, newDirectory + "//" + System.IO.Path.GetFileName(s));

            foreach (PokeStat ps in pokeStats)
            {
                //File.Create(ps.fileLocation);
                using (StreamWriter sw = new StreamWriter(ps.fileLocation))
                {
                    sw.Write(ps.ExportAsASM());
                }
            }
        }
    }
}
