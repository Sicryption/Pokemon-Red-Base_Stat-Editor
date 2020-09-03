using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokeRed_BaseStat_Editor
{
    public enum PokeType
    {
        NORMAL,
        FIGHTING,
        FLYING,
        POISON,
        GROUND,
        ROCK,
        BIRD,
        BUG,
        GHOST,
        FIRE,
        WATER,
        GRASS,
        ELECTRIC,
        PSYCHIC_TYPE,
        ICE,
        DRAGON
    }
    
    enum Move
    {
        TELEPORT,
        NO_MOVE,
        POUND,
        KARATE_CHOP,
        DOUBLESLAP,
        COMET_PUNCH,
        MEGA_PUNCH,
        PAY_DAY,
        FIRE_PUNCH,
        ICE_PUNCH,
        THUNDERPUNCH,
        SCRATCH,
        VICEGRIP,
        GUILLOTINE,
        RAZOR_WIND,
        SWORDS_DANCE,
        CUT,
        GUST,
        WING_ATTACK,
        WHIRLWIND,
        FLY,
        BIND,
        SLAM,
        VINE_WHIP,
        STOMP,
        DOUBLE_KICK,
        MEGA_KICK,
        JUMP_KICK,
        ROLLING_KICK,
        SAND_ATTACK,
        HEADBUTT,
        HORN_ATTACK,
        FURY_ATTACK,
        HORN_DRILL,
        TACKLE,
        BODY_SLAM,
        WRAP,
        TAKE_DOWN,
        THRASH,
        DOUBLE_EDGE,
        TAIL_WHIP,
        POISON_STING,
        TWINEEDLE,
        PIN_MISSILE,
        LEER,
        BITE,
        GROWL,
        ROAR,
        SING,
        SUPERSONIC,
        SONICBOOM,
        DISABLE,
        ACID,
        EMBER,
        FLAMETHROWER,
        MIST,
        WATER_GUN,
        HYDRO_PUMP,
        SURF,
        ICE_BEAM,
        BLIZZARD,
        PSYBEAM,
        BUBBLEBEAM,
        AURORA_BEAM,
        HYPER_BEAM,
        PECK,
        DRILL_PECK,
        SUBMISSION,
        LOW_KICK,
        COUNTER,
        SEISMIC_TOSS,
        STRENGTH,
        ABSORB,
        MEGA_DRAIN,
        LEECH_SEED,
        GROWTH,
        RAZOR_LEAF,
        SOLARBEAM,
        POISONPOWDER,
        STUN_SPORE,
        SLEEP_POWDER,
        PETAL_DANCE,
        STRING_SHOT,
        DRAGON_RAGE,
        FIRE_SPIN,
        THUNDERSHOCK,
        THUNDERBOLT,
        THUNDER_WAVE,
        THUNDER,
        ROCK_THROW,
        EARTHQUAKE,
        FISSURE,
        DIG,
        TOXIC,
        CONFUSION,
        PSYCHIC_M,
        HYPNOSIS,
        MEDITATE,
        AGILITY,
        QUICK_ATTACK,
        RAGE,
        NIGHT_SHADE,
        MIMIC,
        SCREECH,
        DOUBLE_TEAM,
        RECOVER,
        HARDEN,
        MINIMIZE,
        SMOKESCREEN,
        CONFUSE_RAY,
        WITHDRAW,
        DEFENSE_CURL,
        BARRIER,
        LIGHT_SCREEN,
        HAZE,
        REFLECT,
        FOCUS_ENERGY,
        BIDE,
        METRONOME,
        MIRROR_MOVE,
        SELFDESTRUCT,
        EGG_BOMB,
        LICK,
        SMOG,
        SLUDGE,
        BONE_CLUB,
        FIRE_BLAST,
        WATERFALL,
        CLAMP,
        SWIFT,
        SKULL_BASH,
        SPIKE_CANNON,
        CONSTRICT,
        AMNESIA,
        KINESIS,
        SOFTBOILED,
        HI_JUMP_KICK,
        GLARE,
        DREAM_EATER,
        POISON_GAS,
        BARRAGE,
        LEECH_LIFE,
        LOVELY_KISS,
        SKY_ATTACK,
        TRANSFORM,
        BUBBLE,
        DIZZY_PUNCH,
        SPORE,
        FLASH,
        PSYWAVE,
        SPLASH,
        ACID_ARMOR,
        CRABHAMMER,
        EXPLOSION,
        FURY_SWIPES,
        BONEMERANG,
        REST,
        ROCK_SLIDE,
        HYPER_FANG,
        SHARPEN,
        CONVERSION,
        TRI_ATTACK,
        SUPER_FANG,
        SLASH,
        SUBSTITUTE,
        STRUGGLE
    }

    enum TMHM
    {
        MEGA_PUNCH,
        RAZOR_WIND,
        SWORDS_DANCE,
        WHIRLWIND,
        MEGA_KICK,
        TOXIC,
        HORN_DRILL,
        BODY_SLAM,
        TAKE_DOWN,
        DOUBLE_EDGE,
        BUBBLEBEAM,
        WATER_GUN,
        ICE_BEAM,
        BLIZZARD,
        HYPER_BEAM,
        PAY_DAY,
        SUBMISSION,
        COUNTER,
        SEISMIC_TOSS,
        RAGE,
        MEGA_DRAIN,
        SOLARBEAM,
        DRAGON_RAGE,
        THUNDERBOLT,
        THUNDER,
        EARTHQUAKE,
        FISSURE,
        DIG,
        PSYCHIC_M,
        TELEPORT,
        MIMIC,
        DOUBLE_TEAM,
        REFLECT,
        BIDE,
        METRONOME,
        SELFDESTRUCT,
        EGG_BOMB,
        FIRE_BLAST,
        SWIFT,
        SKULL_BASH,
        SOFTBOILED,
        DREAM_EATER,
        SKY_ATTACK,
        REST,
        THUNDER_WAVE,
        PSYWAVE,
        EXPLOSION,
        ROCK_SLIDE,
        TRI_ATTACK,
        SUBSTITUTE,


        CUT,
        FLY,
        SURF,
        STRENGTH,
        FLASH,
        UNUSED
    }

    enum GrowthRate
    {
        GROWTH_MEDIUM_FAST,
        GROWTH_SLIGHTLY_FAST,
        GROWTH_SLIGHTLY_SLOW,
        GROWTH_MEDIUM_SLOW,
        GROWTH_FAST,
        GROWTH_SLOW
    }

    class TMHMClass
    {
        TMHM tmhm;

        public TMHMClass(TMHM tmhmp)
        {
            tmhm = tmhmp;
        }

        public string TMHM
        {
            get { return Enum.GetName(typeof(TMHM), tmhm); }
            //set { tmhm = value; }
        }
    }

    class PokeStat
    {
        public string fileLocation, 
            pokedexID,
            picLocation,
            spriteFrontName,
            spriteBackName,
            name;

        public PokeType type1, type2;
        public List<Move> baseMoves = new List<Move>();
        public ObservableCollection<TMHMClass> tmhms = new ObservableCollection<TMHMClass>();
        public GrowthRate growthRate;

        public int hp, atk, def, spd, spc, 
            catch_rate, baseExp, 
            spriteDimensionX, spriteDimensionY;

        private int dbID = 0;

        public PokeStat(string statLocation)
        {
            fileLocation = statLocation;

            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            name = myTI.ToTitleCase(System.IO.Path.GetFileNameWithoutExtension(statLocation));
            
            bool readingTMHM = false;

            string[] lines = File.ReadAllLines(statLocation);

            for(int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("	", "");
            }
            
            for (int j = 0; j < lines.Length; j++)
            {
                string s = lines[j];

                if (s.StartsWith("db "))
                {
                    s = s.Replace("db ", "");
                    switch (dbID)
                    {
                        //pokedex id
                        case 0:
                            pokedexID = s.Replace(" ; pokedex id", "");
                            break;
                        //hp, atk, def, spd, spc
                        case 1:
                            s = s.Replace(" ", "");

                            string[] intData = s.Split(',');

                            for (int i = 0; i < intData.Length; i++)
                                intData[i] = intData[i];

                            hp = int.Parse(intData[0]);
                            atk = int.Parse(intData[1]);
                            def = int.Parse(intData[2]);
                            spd = int.Parse(intData[3]);
                            spc = int.Parse(intData[4]);
                            break;
                        //type
                        case 2:
                            s = s.Replace(" ; type", "").Replace(" ", "");

                            string[] typeData = s.Split(',');

                            type1 = (PokeType)Enum.Parse(typeof(PokeType), typeData[0]);
                            type2 = (PokeType)Enum.Parse(typeof(PokeType), typeData[1]);

                            break;
                        //catch rate
                        case 3:
                            s = s.Replace(" ; catch rate", "").Replace(" ", "");

                            catch_rate = int.Parse(s);
                            break;
                        //base exp
                        case 4:
                            s = s.Replace(" ; base exp", "").Replace(" ", "");

                            baseExp = int.Parse(s);
                            break;
                        //level 1 moveset
                        case 5:
                            s = s.Replace(" ; level 1 learnset", "").Replace(" ", "");

                            string[] moves = s.Split(',');

                            foreach(string str in moves)
                                baseMoves.Add((Move)Enum.Parse(typeof(Move), str));

                                break;
                        //growth rate
                        case 6:
                            s = s.Replace(" ; growth rate", "").Replace(" ", "");

                            growthRate = (GrowthRate)Enum.Parse(typeof(GrowthRate), s);
                            break;
                        //end of file padding
                        case 7:
                            break;
                    }
                    dbID++;
                }
                else if (s.StartsWith("INCBIN "))
                {
                    s = s.Replace("INCBIN ", "").Replace("; sprite dimensions", "").Replace(" ", "");

                    string[] split = s.Split(',');

                    picLocation = split[0].Replace("\"", "");
                    spriteDimensionX = int.Parse(split[1]);
                    spriteDimensionY = int.Parse(split[2]);
                }
                else if (s.StartsWith("dw "))
                {
                    s = s.Replace("dw ", "").Replace(" ", "");

                    string[] split = s.Split(',');
                    spriteFrontName = split[0];
                    spriteBackName = split[1];
                }
                else if (s.StartsWith("tmhm "))
                    readingTMHM = true;

                if(readingTMHM)
                {
                    if (s.StartsWith("; end"))
                    {
                        readingTMHM = false;
                        continue;
                    }

                    s = s.Replace("tmhm ", "").Replace(" ", "");

                    string[] split = s.Split(',');

                    foreach(string str in split)
                    {
                        if (str == "\\")
                            continue;

                        tmhms.Add(new TMHMClass((TMHM)Enum.Parse(typeof(TMHM), str)));
                    }
                }
            }
        }

        public string ExportAsASM()
        {
            string tmhmSection = "	tmhm";

            for (int i = 0; i < tmhms.Count; i++)
            {
                bool newLineAfter = (i + 1) % 5 == 0;
                bool isMore = tmhms.Count - i > 1;

                if (i > 1 && i % 5 == 0)
                    tmhmSection += "	    ";

                string fifteenCharName = " " + tmhms[i].TMHM;

                fifteenCharName += isMore ? "," : "";

                if (isMore)
                {
                    int missingSpaces = 14 - fifteenCharName.Length;
                    for (int j = 0; j < missingSpaces; j++)
                        fifteenCharName += " ";
                }

                tmhmSection += fifteenCharName;

                if (newLineAfter && isMore)
                    tmhmSection += " \\\n";

                if (!isMore)
                    tmhmSection += "\n";
            }

            tmhmSection += tmhms.Count == 0?"\n	; end\n": "	; end\n";

            return "	db " + pokedexID + " ; pokedex id\n" +
                "\n" +
                "	db  " + hp + ", " + atk + ", " + def + ", " + spd + ", " + spc + "\n" +
                "	; hp atk  def spd  spc\n" +
                "\n" +
                "	db " + Enum.GetName(typeof(PokeType), type1) + ", " + Enum.GetName(typeof(PokeType), type2) + " ; type\n" +
                "	db " + catch_rate + " ; catch rate\n" +
                "	db " + baseExp + " ; base exp\n" +
                "\n" +
                "	INCBIN \"" + picLocation + "\", " + spriteDimensionX + ", " + spriteDimensionY + " ; sprite dimensions\n" +
                "	dw " + spriteFrontName + ", " + spriteBackName + "\n" +
                "\n" +
                "	db " + Enum.GetName(typeof(Move), baseMoves[0]) + ", " + Enum.GetName(typeof(Move), baseMoves[1]) + ", " + Enum.GetName(typeof(Move), baseMoves[2]) + ", " + Enum.GetName(typeof(Move), baseMoves[3]) + " ; level 1 learnset\n" +
                "	db " + Enum.GetName(typeof(GrowthRate), growthRate) + " ; growth rate\n" +
                "\n" +
                "	; tm/hm learnset\n" +
                tmhmSection +
                "\n" +
                "	db 0 ; padding\n";

        }

        public override string ToString()
        {
            return name;
        }
    }
}
