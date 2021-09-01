using Microsoft.EntityFrameworkCore;
using Rennbahn3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Rennbahn3.Logic
{
    class DataLogic
    {
        RennbahnContext RennbahnContext = new RennbahnContext();
        public List<Driver> GetDrivers()
        {
            var drivers = RennbahnContext.Drivers
                .Include(d => d.Team)
                .Include(d => d.Saison)
                .Include(d => d.Results)
                .ToList();

            return drivers;
        }

        public List<Result> GetResults()
        {
            var results = RennbahnContext.Results
                .Include(r => r.Driver)
                .Include(r => r.Race)
                .ToList();

            return results;
        }
        public List<Result> GetResults(Race race)
        {
            var results = RennbahnContext.Results
                .Include(r => r.Driver)
                .Include(r => r.Race)
                .Where(r => r.Race == race)
                .ToList();

            return results;
        }
        //Unused
        /*
        public int GetHighestPositionFromResults()
        {
            int highestposition = RennbahnContext.Results
                .Select(r => r.Position)
                .Max();

            return highestposition;
        }
        */

        public int GetHighestPositionFromResults(Race race)
        {
            int highestposition = 0;
            if (race != null)
            {
                highestposition = RennbahnContext.Results
                .Include(r => r.Race)
                .Where(r => r.Race == race)
                .Select(r => r.Position)
                .Max();
            }
            return highestposition;
        }

        public void RemoveResult(Result result)
        {
            int pos = result.Position;

            RearrangePrecedingPositions(pos, result.Race);
            RennbahnContext.Remove(result);
            RennbahnContext.SaveChanges();
        }

        public void RearrangePrecedingPositions(int pos, Race race)
        {
            int countraces = GetResults(race).Count;
            //Position to move back
            pos++;

            for (int i = pos; i <= countraces; i++)
            {
                    var result = RennbahnContext.Results
                    .Include(r => r.Race)
                    .Where(r => r.Position == i)
                    .Where(r => r.Race == race)
                    .First();
                    result.Position = result.Position - 1;
            }
        }

        public void MoveUpPosition(Result result)
        {
            int precedingpos = 0;
            if (result.Position != 1)
            {
                Result PrecedingResult = new Result();
                PrecedingResult = GetPrecedingResult(result);
                precedingpos = PrecedingResult.Position;

                PrecedingResult.Position = result.Position;
                result.Position = precedingpos;
                RennbahnContext.SaveChanges();
            }
        }

        public void MoveDownPosition(Result result)
        {
            int succeedingpos = 0;
            if(result.Position != GetResults(result.Race).Count)
            {
                Result SucceedingResult = new Result();
                SucceedingResult = GetSucceedingResult(result);
                succeedingpos = SucceedingResult.Position;

                SucceedingResult.Position = result.Position;
                result.Position = succeedingpos;
                RennbahnContext.SaveChanges();
            }
        }

        public Result GetSucceedingResult(Result result)
        {
            var res = RennbahnContext.Results
                .Include(r => r.Race)
                .Where(r => r.Position == result.Position + 1)
                .Where(r => r.Race == result.Race)
                .First();
            return res;
        }

        public Result GetPrecedingResult(Result result)
        {
            var res = RennbahnContext.Results
                    .Include(r => r.Race)
                    .Where(r => r.Position == result.Position - 1)
                    .Where(r => r.Race == result.Race)
                    .First();
            return res;
        }

        public List<Race> GetRaces()
        {
            var races = RennbahnContext.Races
                .Include(r => r.Saison)
                .Include(r => r.Results)
                .ToList();
            return races;
        }

        public void AddResult(Result result)
        {
            RennbahnContext.Add(result);
            RennbahnContext.SaveChanges();
        }
    }
}
