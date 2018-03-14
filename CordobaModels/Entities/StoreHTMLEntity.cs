using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class StoreHTMLEntity
    {
        public List<StoreSummary> storeSummary { get; set; }
        public List<PointsRemaining> pointsRemaining { get; set; }
        public List<ParticipantsLoadedByMonth> participantsLoadedByMonth { get; set; }
        public List<PointsLoadedByMonth> pointsLoadedByMonth { get; set; }
        public List<PointsRedeemedByMonth> pointsRedeemedByMonth { get; set; }
    }

    public class StoreSummary
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class PointsRemaining
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class ParticipantsLoadedByMonth
    {
        public string Month { get; set; }
        public int CustomerCount { get; set; }
    }

    public class PointsLoadedByMonth
    {
        public string Month { get; set; }
        public int Points { get; set; }
    }

    public class PointsRedeemedByMonth
    {
        public string Month { get; set; }
        public int Points { get; set; }
    }
}
