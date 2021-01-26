﻿using HeatMap;
using Newtonsoft.Json;
using System.Collections.Generic;
using Utilities;

namespace WorldMap
{
    public class GlobalWorldMap
    {
        public string Type = "GlobalWorldMap";
        public int TeamId;
        public int timeStampMs;
        public GameState gameState = GameState.STOPPED;
        public StoppedGameAction stoppedGameAction = StoppedGameAction.NONE;
        public List<Location> ballLocationList { get; set; }
        public Dictionary<int, Location> teammateLocationList { get; set; }
        public Dictionary<int, Location> teammateGhostLocationList { get; set; }
        public Dictionary<int, Location> teammateDestinationLocationList { get; set; }
        public Dictionary<int, Location> teammateWayPointList { get; set; }
        public List<Location> opponentLocationList { get; set; }
        public List<LocationExtended> obstacleLocationList { get; set; }

        public GlobalWorldMap()
        {
        }
        public GlobalWorldMap(int teamId)
        {
            TeamId = teamId;
        }

        public WorldStateMessage ConvertToWorldStateMessage()
        {
            WorldStateMessage wsm = new WorldStateMessage();
            foreach(var teamMate in teammateLocationList)
            {
                Robot r = new Robot();
                r.Id = teamMate.Key;
                r.Pose = new List<double>() { teamMate.Value.X, teamMate.Value.Y, teamMate.Value.Theta };
                r.TargetPose = new List<double>() { 0, 0, 0 };
                r.Velocity = new List<double>() { teamMate.Value.Vx, teamMate.Value.Vy, teamMate.Value.Vtheta };
                r.Intention = "";
                r.BatteryLevel = 100;
                r.BallEngaged = 0;
                wsm.Robots.Add(r);
            }

            //On prend par défaut la première balle du premier robot
            Ball b = new Ball();
            b.Position = new List<double?>() { ballLocationList[0].X, ballLocationList[0].X, 0};
            b.Velocity = new List<double?>() { ballLocationList[0].Vx, ballLocationList[0].Vy, 0 };
            b.Confidence = 1;
            wsm.Balls.Add(b);

            foreach (var o in obstacleLocationList)
            {
                Obstacle obstacle = new Obstacle();
                obstacle.Position = new List<double>() { o.X, o.Y};
                obstacle.Velocity = new List<double>() { o.Vx, o.Vy};
                obstacle.Radius = 0.5;
                obstacle.Confidence = 1;
                wsm.Obstacles.Add(obstacle);
            }

            wsm.Intention = "Win";
            wsm.AgeMs = timeStampMs;
            wsm.TeamName = "RCT";
            wsm.Type = "worldstate";
            return wsm;
        }


    }
    public class LocalWorldMap
    {
        public string Type = "LocalWorldMap";
        public int RobotId = 0;
        public int TeamId = 0;
        public Location robotLocation { get; set; }
        public Location robotGhostLocation { get; set; }
        public Location destinationLocation { get; set; }
        public Location waypointLocation { get; set; }
        public List<Location> ballLocationList { get; set; }
        public List<LocationExtended> obstaclesLocationList { get; set; }
        public List<PolarPointListExtended> lidarObjectList { get; set; }

        [JsonIgnore] 
        public List<PointD> lidarMap { get; set; }
        [JsonIgnore]
        public Heatmap heatMapStrategy { get; set; }
        public Heatmap heatMapWaypoint { get; set; }

        public LocalWorldMap()
        {
        }        
    }



    public enum GameState
    {
        STOPPED,
        STOPPED_GAME_POSITIONING,
        PLAYING,
    }

    public enum StoppedGameAction
    {
        NONE,
        KICKOFF,
        KICKOFF_OPPONENT,
        FREEKICK,
        FREEKICK_OPPONENT,
        GOALKICK,
        GOALKICK_OPPONENT,
        THROWIN,
        THROWIN_OPPONENT,
        CORNER,
        CORNER_OPPONENT,
        PENALTY,
        PENALTY_OPPONENT,
        PARK,
        DROPBALL,
        GOTO_0_1,
        GOTO_0_1_OPPONENT,
        GOTO_1_0,
        GOTO_1_0_OPPONENT,
        GOTO_0_M1,
        GOTO_0_M1_OPPONENT,
        GOTO_M1_0,
        GOTO_M1_0_OPPONENT,
        GOTO_0_0,
        GOTO_0_0_OPPONENT,
    }
}
