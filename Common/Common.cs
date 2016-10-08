using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kardashit
{
    static public class Common
    {
        public static List<string> seasons = new List<string>(new string[] { "SeasonA_Y1", "SeasonB_Y1", "SeasonA_Y2", "SeasonB_Y2", "SeasonA_Y3", });
        public static List<string> products = new List<string>(new string[] { "Product8", "Product6", "Product7", "Product10", "Product5", "Product11", "Product14", "Product15", "Product18", "Product19", "Product20", "Product16", "Product2", "Product4", "Product26", "Product21", "Product25", "Product3", "Product30", "Product31", "Product32", "Product33", "Product22", "Product34", "Product1", "Product35", "Product36", "Product13", "Product37", "Product12", "Product40", "Product41", "Product9", "Product45", "Product42", "Product46", "Product48", "Product38", "Product50", "Product52", "Product53", "Product54", "Product24", "Product58", "Product60", "Product62", "Product66", "Product69", "Product70", "Product65", "Product61", "Product57", "Product67", "Product72", "Product73", "Product74", "Product77", "Product75", "Product76", "Product43", "Product64", "Product79", "Product71", "Product80", "Product82", "Product83", "Product84", "Product86", "Product56", "Product87", "Product47", "Product91", "Product92", "Product94", "Product68", "Product95", "Product97", "Product23", "Product98", "Product93", "Product90", "Product102", "Product27", "Product101", "Product29", "Product51", "Product106", "Product108", "Product109", "Product113", "Product28", "Product114", "Product17", "Product123", "Product39", "Product100", "Product124", "Product119", "Product110", "Product126", "Product44", "Product81", "Product127", "Product107", "Product49", "Product85", "Product99", "Product118", });
        public static List<string> cycles = new List<string>(new string[] { "Cycle1", "Cycle3", "Cycle2", });
        public static List<string> countries = new List<string>(new string[] { "Country1", "Country3", "Country2", "Country4", "Country5", "Country6", "Country8", "Country7", "Country13", "Country14", "Country16", "Country9", "Country18", "Country17", "Country19", "Country20", });
        public static List<string> bizUnits = new List<string>(new string[] { "Biz_unit3", "Biz_unit4", });
        public static List<string> cats = new List<string>(new string[] { "Cat3", "Cat4", "Cat8", "Cat10", "Cat11", "Cat13", "Cat15", });
        public static List<string> subCats = new List<string>(new string[] { "SubCat6", "SubCat8", "SubCat9", "SubCat10", "SubCat11", "SubCat12", "SubCat15", "SubCat16", "SubCat20", "SubCat24", "SubCat25", "SubCat26", "SubCat28", "SubCat29", "SubCat30", "SubCat33", "SubCat36", "SubCat40", "SubCat41", "SubCat43", "SubCat46", "SubCat48", "SubCat61", "SubCat57", "SubCat53", "SubCat67", "SubCat68", "SubCat69", "SubCat72", "SubCat73", "SubCat74", "SubCat37", "SubCat77", "SubCat80", });
        public static List<string> unitBases = new List<string>(new string[] { "UnitBase10", "UnitBase12", "UnitBase13", "UnitBase14", "UnitBase15", "UnitBase16", "UnitBase17", "UnitBase18", "UnitBase24", "UnitBase25", "UnitBase26", "UnitBase27", "UnitBase35", "UnitBase36", "UnitBase37", "UnitBase38", "UnitBase39", "UnitBase44", "UnitBase45", "UnitBase46", "UnitBase54", "UnitBase55", "UnitBase56", "UnitBase57", "UnitBase58", "UnitBase59", "UnitBase60", "UnitBase61", "UnitBase74", "UnitBase75", "UnitBase77", "UnitBase79", "UnitBase80", "UnitBase81", "UnitBase82", "UnitBase83", "UnitBase84", "UnitBase85", "UnitBase86", "UnitBase89", "UnitBase90", "UnitBase91", "UnitBase93", "UnitBase94", "UnitBase95", "UnitBase97", "UnitBase98", "UnitBase99", "UnitBase100", "UnitBase101", "UnitBase102", "UnitBase104", "UnitBase105", "UnitBase106", "UnitBase107", "UnitBase108", "UnitBase109", "UnitBase110", "UnitBase111", "UnitBase112", "UnitBase113", "UnitBase114", "UnitBase115", "UnitBase116", "UnitBase117", "UnitBase118", "UnitBase119", "UnitBase120", "UnitBase121", "UnitBase122", "UnitBase123", "UnitBase126", "UnitBase127", "UnitBase131", "UnitBase134", "UnitBase142", "UnitBase143", "UnitBase150", "UnitBase151", "UnitBase152", "UnitBase153", "UnitBase154", "UnitBase155", "UnitBase156", "UnitBase157", "UnitBase159", "UnitBase160", "UnitBase161", "UnitBase162", "UnitBase163", "UnitBase164", "UnitBase165", "UnitBase166", "UnitBase167", "UnitBase170", "UnitBase171", "UnitBase174", "UnitBase175", "UnitBase179", "UnitBase186", "UnitBase188", "UnitBase189", "UnitBase190", "UnitBase194", "UnitBase195", "UnitBase197", "UnitBase199", "UnitBase200", "UnitBase201", "UnitBase205", "UnitBase210", "UnitBase211", "UnitBase214", "UnitBase215", "UnitBase216", "UnitBase217", "UnitBase193", "UnitBase222", "UnitBase223", "UnitBase224", "UnitBase230", "UnitBase187", "UnitBase231", "UnitBase232", "UnitBase233", "UnitBase234", "UnitBase239", "UnitBase241", "UnitBase242", "UnitBase243", "UnitBase246", "UnitBase247", "UnitBase248", "UnitBase249", "UnitBase250", "UnitBase251", "UnitBase258", "UnitBase259", "UnitBase212", "UnitBase213", "UnitBase265", "UnitBase270", "UnitBase271", "UnitBase272", "UnitBase273", "UnitBase274", "UnitBase276", "UnitBase277", "UnitBase282", "UnitBase283", "UnitBase284", "UnitBase286", "UnitBase264", "UnitBase287", "UnitBase288", "UnitBase289", "UnitBase290", "UnitBase291", "UnitBase204", "UnitBase203", "UnitBase301", "UnitBase302", "UnitBase303", "UnitBase304", "UnitBase305", "UnitBase307", "UnitBase308", "UnitBase310", "UnitBase311", "UnitBase312", "UnitBase313", "UnitBase315", "UnitBase316", "UnitBase318", "UnitBase319", "UnitBase103", "UnitBase260", "UnitBase255", "UnitBase323", "UnitBase324", "UnitBase325", "UnitBase326", "UnitBase327", "UnitBase328", "UnitBase330", "UnitBase335", "UnitBase336", "UnitBase337", "UnitBase338", "UnitBase339", "UnitBase340", "UnitBase343", "UnitBase344", "UnitBase345", "UnitBase348", "UnitBase349", "UnitBase351", "UnitBase359", "UnitBase363", "UnitBase365", "UnitBase366", "UnitBase370", "UnitBase371", "UnitBase372", "UnitBase244", "UnitBase373", "UnitBase376", "UnitBase377", "UnitBase378", "UnitBase379", "UnitBase380", "UnitBase381", "UnitBase382", "UnitBase383", "UnitBase384", "UnitBase385", "UnitBase386", "UnitBase387", "UnitBase388", "UnitBase389", "UnitBase390", "UnitBase391", "UnitBase394", "UnitBase396", "UnitBase397", "UnitBase402", "UnitBase403", "UnitBase404", "UnitBase405", "UnitBase406", "UnitBase407", "UnitBase408", "UnitBase409", "UnitBase414", "UnitBase415", "UnitBase416", "UnitBase422", "UnitBase425", "UnitBase426", "UnitBase427", "UnitBase429", "UnitBase431", "UnitBase432", "UnitBase434", "UnitBase435", "UnitBase436", "UnitBase437", "UnitBase438", "UnitBase439", "UnitBase135", "UnitBase441", "UnitBase444", "UnitBase445", "UnitBase447", "UnitBase448", "UnitBase449", "UnitBase450", "UnitBase196", "UnitBase485", "UnitBase465", "UnitBase486", "UnitBase487", "UnitBase488", "UnitBase489", "UnitBase458", "UnitBase459", "UnitBase457", "UnitBase492", "UnitBase474", "UnitBase496", "UnitBase498", "UnitBase500", "UnitBase502", "UnitBase503", "UnitBase346", "UnitBase506", "UnitBase507", "UnitBase508", "UnitBase509", "UnitBase510", "UnitBase511", "UnitBase512", "UnitBase513", "UnitBase514", "UnitBase515", "UnitBase517", "UnitBase518", "UnitBase455", "UnitBase467", "UnitBase468", "UnitBase522", "UnitBase461", "UnitBase523", "UnitBase421", "UnitBase456", "UnitBase525", "UnitBase526", "UnitBase527", "UnitBase476", "UnitBase530", "UnitBase537", "UnitBase538", "UnitBase484", "UnitBase539", "UnitBase541", "UnitBase543", "UnitBase544", "UnitBase478", "UnitBase463", "UnitBase535", "UnitBase374", "UnitBase551", "UnitBase521", "UnitBase545", "UnitBase553", "UnitBase520", "UnitBase549", "UnitBase534", "UnitBase542", "UnitBase546", "UnitBase561", "UnitBase529", });
        public static List<string> genders = new List<string>(new string[] { "Gender1", "Gender2", "Gender3", });
        public static List<string> groups = new List<string>(new string[] { "Group5", "Group6", "Group2", "Group8", "Group9", "Group10", "Group14", });
        public static List<string> colours = new List<string>(new string[] { "Colour10", "Colour8", "Colour1", "Colour5", "Colour2", "Colour11", "Colour13", "Colour4", "Colour3", "Colour9", "Colour6", "Colour14", "Colour7", "Colour15", "Colour12", "Colour16", });
        public static List<string> distPhases = new List<string>(new string[] { "0", "4", "2", "1", "3", "5", "12", "13", "7", "9", "8", "20", "11", "15", "6", "21", "18", "17", "29", "34", "14", "35", "19", "48", "10", "16", "22", "28", "65", "40", "33", "46", "39", "109", "31", "158", "81", "66", "73", "68", "114", "95", "43", "24", "71", "23", "45", "27", "32", "41", "136", "125", "26", "30", "54", "37", "187", "137", "86", });

        public static double[][] ReadInputCSV(string file_path, bool ignore_first_line = false)
        {
            var lines = File.ReadAllLines(file_path);
            var list = new double[ignore_first_line ? lines.Length - 1 : lines.Length][];
            int j = 0;

            foreach (var line in lines)
            {
                if (ignore_first_line)
                {
                    ignore_first_line = false;
                    continue;
                }

                var t = Regex.Split(line, ",");

                var data = new double[t.Length];

                for (int i = 0; i < t.Length; ++i)
                    data[i] = double.Parse(t[i], System.Globalization.CultureInfo.InvariantCulture);

                list[j++] = data;
            }

            return list;
        }

        public static void MakeClothesFileLists(string file_path)
        {
            // ID,SeasonCode,ProductCode,CycleCode,CountryCode,Biz_unit_Code,CatCode,SubCatCode,UnitBaseCode,GenderCode,GroupCode,ColourCode,First_Sale_Date,PVP,Dist_phase,Units_Sold,Stock,Flop
            var lines = File.ReadAllLines(file_path);
            bool first_line = true;

            foreach (var line in lines)
            {
                if (first_line)
                {
                    first_line = false;
                    continue;
                }

                var t = Regex.Split(line, ",");

                if (!seasons.Contains(t[1]))
                    seasons.Add(t[1]);

                if (!products.Contains(t[2]))
                    products.Add(t[2]);

                if (!cycles.Contains(t[3]))
                    cycles.Add(t[3]);

                if (!countries.Contains(t[4]))
                    countries.Add(t[4]);

                if (!bizUnits.Contains(t[5]))
                    bizUnits.Add(t[5]);

                if (!cats.Contains(t[6]))
                    cats.Add(t[6]);

                if (!subCats.Contains(t[7]))
                    subCats.Add(t[7]);

                if (!unitBases.Contains(t[8]))
                    unitBases.Add(t[8]);

                if (!genders.Contains(t[9]))
                    genders.Add(t[9]);

                if (!groups.Contains(t[10]))
                    groups.Add(t[10]);

                if (!colours.Contains(t[11]))
                    colours.Add(t[11]);

                if (!distPhases.Contains(t[14]))
                    distPhases.Add(t[14]);
            }

            Console.Write("static List<string> seasons = new List<string>(new string[]{");
            foreach (var i in seasons)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> products = new List<string>(new string[]{");
            foreach (var i in products)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> cycles = new List<string>(new string[]{");
            foreach (var i in cycles)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> countries = new List<string>(new string[]{");
            foreach (var i in countries)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> bizUnits = new List<string>(new string[]{");
            foreach (var i in bizUnits)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> cats = new List<string>(new string[]{");
            foreach (var i in cats)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> subCats = new List<string>(new string[]{");
            foreach (var i in subCats)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> unitBases = new List<string>(new string[]{");
            foreach (var i in unitBases)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> genders = new List<string>(new string[]{");
            foreach (var i in genders)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> groups = new List<string>(new string[]{");
            foreach (var i in groups)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> colours = new List<string>(new string[]{");
            foreach (var i in colours)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");

            Console.Write("static List<string> distPhases = new List<string>(new string[]{");
            foreach (var i in distPhases)
                Console.Write("\"" + i + "\", ");
            Console.WriteLine("});");
        }

        static void Normalize(double[][] dataMatrix, int[] cols, int size)
        {
            if (size == -1)
                size = dataMatrix.Length;

            // normalize specified cols by computing (x - mean) / sd for each value
            foreach (int col in cols)
            {
                double sum = 0.0;
                for (int i = 0; i < size; ++i)
                    sum += dataMatrix[i][col];
                double mean = sum / size;
                sum = 0.0;
                for (int i = 0; i < size; ++i)
                    sum += (dataMatrix[i][col] - mean) * (dataMatrix[i][col] - mean);
                // thanks to Dr. W. Winfrey, Concord Univ., for catching bug in original code
                double sd = Math.Sqrt(sum / (size - 1));
                for (int i = 0; i < size; ++i)
                    dataMatrix[i][col] = (dataMatrix[i][col] - mean) / sd;
            }
        }

        public static double[][] ParseClothesFile(string file_path, out int n_lines)
        {
            // ID,SeasonCode,ProductCode,CycleCode,CountryCode,Biz_unit_Code,CatCode,SubCatCode,UnitBaseCode,GenderCode,GroupCode,ColourCode,First_Sale_Date,PVP,Dist_phase,Units_Sold,Stock,Flop
            var lines = File.ReadAllLines(file_path);
            var list = new double[lines.Length - 1][];
            n_lines = 0;
            bool first_line = true;

            foreach (var line in lines)
            {
                if (first_line)
                {
                    first_line = false;
                    continue;
                }

                var t = Regex.Split(line, ",");

                var data = new double[19];
                data[0] = (double)seasons.IndexOf(t[1]) / (seasons.Count - 1) * 2.0 - 1.0;      // season code
                data[1] = (double)products.IndexOf(t[2]) / (products.Count - 1) * 2.0 - 1.0;    // product code
                data[2] = (double)cycles.IndexOf(t[3]) / (cycles.Count - 1) * 2.0 - 1.0;        // cycle code
                data[3] = (double)countries.IndexOf(t[4]) / (countries.Count - 1) * 2.0 - 1.0;  // country code
                data[4] = (double)bizUnits.IndexOf(t[5]) / (bizUnits.Count - 1) * 2.0 - 1.0;    // business unit code
                data[5] = (double)cats.IndexOf(t[6]) / (cats.Count - 1) * 2.0 - 1.0;            // category code
                data[6] = (double)subCats.IndexOf(t[7]) / (subCats.Count - 1) * 2.0 - 1.0;      // sub category code
                data[7] = (double)unitBases.IndexOf(t[8]) / (unitBases.Count - 1) * 2.0 - 1.0;  // unit base code
                data[8] = (double)genders.IndexOf(t[9]) / (genders.Count - 1) * 2.0 - 1.0;      // gender code
                data[9] = (double)groups.IndexOf(t[10]) / (groups.Count - 1) * 2.0 - 1.0;       // group code
                data[10] = (double)colours.IndexOf(t[11]) / (colours.Count - 1) * 2.0 - 1.0;    // colour code

                if (t[12].Length >= 8)
                {
                    int year = int.Parse(t[12].Substring(0, 4));
                    int month = int.Parse(t[12].Substring(4, 2));
                    int day = int.Parse(t[12].Substring(6, 2));

                    var timestamp = (new DateTime(year, month, day) - new DateTime(year, 1, 1)).TotalDays / (new DateTime(year, 12, 31) - new DateTime(year, 1, 1)).TotalDays;
                    timestamp = Math.Abs(timestamp - 0.5) * 4.0 - 1.0;

                    data[11] = timestamp;
                    data[12] = 1.0;
                }
                else
                {
                    data[11] = 0.0;
                    data[12] = -1.0;
                }

                data[13] = double.Parse(t[13], System.Globalization.CultureInfo.InvariantCulture);  // pvp
                data[14] = (double)distPhases.IndexOf(t[14]) / (distPhases.Count - 1) * 2.0 - 1.0;  // dist phase
                data[15] = double.Parse(t[15], System.Globalization.CultureInfo.InvariantCulture);  // units sold
                data[16] = double.Parse(t[16], System.Globalization.CultureInfo.InvariantCulture);  // stock

                if (t.Length >= 18)
                {
                    // output
                    data[17] = double.Parse(t[17], System.Globalization.CultureInfo.InvariantCulture);  // flop
                    data[18] = 1.0 - data[17];                                                          // not flop
                }

                list[n_lines++] = data;
            }

            Normalize(list, new int[] { 13, 15, 16 }, n_lines);

            return list;
        }

        public static double[] ReadWeightFile(string file_path)
        {
            using (Stream stream = File.Open(file_path, FileMode.Open))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                return (double[])bformatter.Deserialize(stream);
            }
        }

        public static void ShowVector(double[] vector, int valsPerRow, int decimals, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
            {
                if (i % valsPerRow == 0) Console.WriteLine("");
                Console.Write(vector[i].ToString("F" + decimals).PadLeft(decimals + 4) + " ");
            }
            if (newLine == true) Console.WriteLine("");
        }

        public static void ShowMatrix(double[][] matrix, int numRows, int decimals, bool newLine)
        {
            int rows = numRows < matrix.Length ? numRows : matrix.Length;

            for (int i = 0; i < rows; ++i)
            {
                Console.Write(i.ToString().PadLeft(3) + ": ");
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    if (matrix[i][j] >= 0.0) Console.Write(" "); else Console.Write("-");
                    Console.Write(Math.Abs(matrix[i][j]).ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine == true)
                Console.WriteLine("");
        }

        public static void WriteWeightFile(double[] weights, string file_path)
        {
            using (Stream stream = File.Open(file_path, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, weights);
            }
        }
    }
}
