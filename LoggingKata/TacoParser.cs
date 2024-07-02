namespace LoggingKata
{
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Cell length is too short");
                return null; 
            }

            var lat1 = double.Parse(cells[0]);

            var long1 = double.Parse(cells[1]);

            var storeName = cells[2];

            Point point = new Point();
            point.Latitude = lat1;
            point.Longitude = long1;

            TacoBell newTacoBell = new TacoBell();

            newTacoBell.Name = storeName;
            newTacoBell.Location = point;


            return newTacoBell;
        }
    }
}
