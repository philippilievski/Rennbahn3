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

        public List<Saison> GetSaisons()
        {
            var saisons = RennbahnContext.Saisons
                .Include(s => s.Drivers)
                .Include(s => s.Races)
                .ToList();

            return saisons;
        }

        public List<Team> GetTeams()
        {
            var teams = RennbahnContext.Teams
                .Include(t => t.Drivers)
                .ToList();

            return teams;
        }

        public void AddDriver(Driver driver)
        {
            RennbahnContext.Add(driver);
            RennbahnContext.SaveChanges();
        }

        /// <summary>
        /// Gets max position from results table
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes result from table
        /// </summary>
        /// <param name="result"></param>
        public void RemoveResult(Result result)
        {
            int pos = result.Position;

            RearrangePrecedingPositions(pos, result.Race);
            RennbahnContext.Remove(result);
            RennbahnContext.SaveChanges();
        }

        /// <summary>
        /// Moves all positions from preceding races one number closer to first position
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="race"></param>
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

        /// <summary>
        /// Swaps position of input result with the position of preceding result
        /// </summary>
        /// <param name="result"></param>
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

        /// <summary>
        /// Swaps position of input result with the position of succeeding result
        /// </summary>
        /// <param name="result"></param>
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

        /// <summary>
        /// Iterates through all the results and gives points to drivres
        /// </summary>
        public void UpdatePoints()
        {
            List<Result> results = GetResults();
            List<Driver> drivers = GetDrivers();

            foreach (var driver in drivers)
            {
                driver.Points = 0;
            }
            foreach(var result in results)
            {
                switch (result.Position)
                {
                    case 1:
                        result.Driver.Points += 10;
                        break;
                    case 2:
                        result.Driver.Points += 8;
                        break;
                    case 3:
                        result.Driver.Points += 7;
                        break;
                    case 4:
                        result.Driver.Points += 6;
                        break;
                    case 5:
                        result.Driver.Points += 3;
                        break;
                    case 6:
                        result.Driver.Points += 1;
                        break;
                    default:
                        result.Driver.Points += 0;
                        break;
                }
            }
            RennbahnContext.SaveChanges();
        }

        /// <summary>
        /// Finds the result with the position + 1 of the input result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public Result GetSucceedingResult(Result result)
        {
            var res = RennbahnContext.Results
                .Include(r => r.Race)
                .Where(r => r.Position == result.Position + 1)
                .Where(r => r.Race == result.Race)
                .First();
            return res;
        }

        /// <summary>
        /// Finds the result with the position - 1 of the input result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public Result GetPrecedingResult(Result result)
        {
            var res = RennbahnContext.Results
                    .Include(r => r.Race)
                    .Where(r => r.Position == result.Position - 1)
                    .Where(r => r.Race == result.Race)
                    .First();
            return res;
        }

        /// <summary>
        /// Writes all races to list
        /// </summary>
        /// <returns>List of Races</returns>
        public List<Race> GetRaces()
        {
            var races = RennbahnContext.Races
                .Include(r => r.Saison)
                .Include(r => r.Results)
                .ToList();
            return races;
        }

        /// <summary>
        /// Writes all races of a specific saison to list
        /// </summary>
        /// <param name="saison"></param>
        /// <returns>List of races</returns>
        public List<Race> GetRaces(Saison saison)
        {
            var races = RennbahnContext.Races
                .Include(r => r.Saison)
                .Include(r => r.Results)
                .Where(r => r.Saison == saison)
                .ToList();
            return races;
        }

        /// <summary>
        /// Add result to database
        /// </summary>
        /// <param name="result"></param>
        public void AddResult(Result result)
        {
            RennbahnContext.Add(result);
            RennbahnContext.SaveChanges();
        }
    }
}
