using System;

namespace GarageServer
{
    [Serializable]
    public class Car
    {
        public string CarNumber { get; set; }
        public string CarVIN { get; set; }
        public string Yad { get; set; }
        public DateTime RoadDate { get; set; }
        public string ShildaNumber { get; set; }
        public string CarColor { get; set; }

        public string CodeTable { get; set; }
        public string ProductSymbol { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string CommericalAlias { get; set; }
        public string EngineCapacity { get; set; }
        public string TotalWeight { get; set; }
        public string Propulsion { get; set; }
        public string AC { get; set; }
        public string ABS { get; set; }
        public string AirBags { get; set; }
        public string PowerSteering { get; set; }
        public string Gaer { get; set; }
        public string PowerWindow { get; set; }
        public string Roof { get; set; }
        public string MagnesiumWheels { get; set; }
        public string Box { get; set; }
        public string Body { get; set; }
        public string TrimLevel { get; set; }
        public string FuelType { get; set; }
        public string DorNumber { get; set; }
        public string HorsePower { get; set; }
        public string SeatNumber { get; set; }
        public string ESP { get; set; }
        public string TowingCapacityWithBrakes { get; set; }
        public string TowingCapacityWithoutBrakes { get; set; }
        public string RegulatoryType { get; set; }
        public string ConverterType { get; set; }
        public string Hiberdi { get; set; }
        public string GreenIndex { get; set; }
        public string InfectionGroup { get; set; }
        public string ControlDeviationFromPath { get; set; }
        public string BounceForward { get; set; }
        public string IdentificationOfDeadArea { get; set; }
        public string AdaptiveCruiseControl { get; set; }
        public string IdentificationOfPedestrians { get; set; }
        public string BrakeSystem { get; set; }
        public string ReverseCamera { get; set; }
        public string TirePressureSensors { get; set; }
        public string BeltSensors { get; set; }
        public string SafetyScore { get; set; }
        public string LevelOfSafetyAccessories { get; set; }
        public string AutoForwardLighting { get; set; }
        public string AutomaticControlOfHighLights { get; set; }
        public string IdentifyingDangerousZoomingMode { get; set; }
        public string IdentifyingTrafficSigns { get; set; }
        public string Year { get; set; }
        public string AgraGroup { get; set; }
        public string ownerShip { get; set; }
    }
}