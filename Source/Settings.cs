﻿/**
 * Settings.cs
 * 
 * Thunder Aerospace Corporation's Life Support for the Kerbal Space Program, by Taranis Elsu
 * 
 * (C) Copyright 2013, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tac
{
    public class Settings
    {
        public string Food { get; private set; }
        public string Water { get; private set; }
        public string Oxygen { get; private set; }
        public string Electricity { get { return "ElectricCharge"; } }
        public string CO2 { get; private set; }
        public string Waste { get; private set; }
        public string WasteWater { get; private set; }

        public double FoodConsumptionRate { get; set; }
        public double WaterConsumptionRate { get; set; }
        public double OxygenConsumptionRate { get; set; }
        public double ElectricityConsumptionRate { get; set; }
        public double BaseElectricityConsumptionRate { get; set; }
        public double EvaElectricityConsumptionRate { get; set; }
        public double CO2ProductionRate { get; set; }
        public double WasteProductionRate { get; set; }
        public double WasteWaterProductionRate { get; set; }

        public double MaxTimeWithoutFood { get; set; }
        public double MaxTimeWithoutWater { get; set; }
        public double MaxTimeWithoutOxygen { get; set; }
        public double MaxTimeWithoutElectricity { get; set; }

        public double DefaultResourceAmount { get; set; }
        public double EvaDefaultResourceAmount { get; set; }

        public int FoodId
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(Food).id;
            }
        }
        public int WaterId
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(Water).id;
            }
        }
        public int OxygenId
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(Oxygen).id;
            }
        }
        public int ElectricityId
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(Electricity).id;
            }
        }
        public int CO2Id
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(CO2).id;
            }
        }
        public int WasteId
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(Waste).id;
            }
        }
        public int WasteWaterId
        {
            get
            {
                return PartResourceLibrary.Instance.GetDefinition(WasteWater).id;
            }
        }

        public bool AllowCrewRespawn
        {
            get
            {
                return HighLogic.CurrentGame.Parameters.Difficulty.MissingCrewsRespawn;
            }
            set
            {
                HighLogic.CurrentGame.Parameters.Difficulty.MissingCrewsRespawn = value;
            }
        }
        public double RespawnDelay { get; set; }

        public Settings()
        {
            const int SECONDS_PER_MINUTE = 60;
            const int SECONDS_PER_HOUR = 60 * SECONDS_PER_MINUTE;
            const int SECONDS_PER_DAY = 24 * SECONDS_PER_HOUR;

            Food = "Food_TAC";
            Water = "Water_TAC";
            Oxygen = "Oxygen_TAC";
            CO2 = "CO2_TAC";
            Waste = "Waste_TAC";
            WasteWater = "WasteWater_TAC";

            // Consumption rates in units per Earth second
            // See the TacResources.cfg for conversions between units and kg.
            // FIXME add my calculations and references
            FoodConsumptionRate = 1.0 / SECONDS_PER_DAY;
            WaterConsumptionRate = 1.0 / SECONDS_PER_DAY;
            OxygenConsumptionRate = 1.0 / SECONDS_PER_DAY;
            ElectricityConsumptionRate = 1200.0 / SECONDS_PER_DAY;
            BaseElectricityConsumptionRate = 2400.0 / SECONDS_PER_DAY;
            EvaElectricityConsumptionRate = 100.0 / (SECONDS_PER_DAY / 2.0); // 100 per 12 hours (1/2 day)
            CO2ProductionRate = 1.0 / SECONDS_PER_DAY;
            WasteProductionRate = 1.0 / SECONDS_PER_DAY;
            WasteWaterProductionRate = 1.0 / SECONDS_PER_DAY;

            MaxTimeWithoutFood = 30.0 * SECONDS_PER_DAY; // 30 days
            MaxTimeWithoutWater = 3.0 * SECONDS_PER_DAY; // 3 days
            MaxTimeWithoutOxygen = 5.0 * SECONDS_PER_MINUTE; // 5 minutes
            MaxTimeWithoutElectricity = 2.0 * SECONDS_PER_HOUR; // 2 hours

            // Amount of resources to load crewable parts with, in seconds
            DefaultResourceAmount = 3.0 * SECONDS_PER_DAY; // 3 days
            EvaDefaultResourceAmount = 12.0 * SECONDS_PER_HOUR; // 12 hours (1/2 day)

            RespawnDelay = 365 * SECONDS_PER_DAY; // 1 year (default is too short at only 36 minutes)
        }

        public void Load(ConfigNode config)
        {
            Food = Utilities.GetValue(config, "Food", Food);
            Water = Utilities.GetValue(config, "Water", Water);
            Oxygen = Utilities.GetValue(config, "Oxygen", Oxygen);
            CO2 = Utilities.GetValue(config, "CO2", CO2);
            Waste = Utilities.GetValue(config, "Waste", Waste);
            WasteWater = Utilities.GetValue(config, "WasteWater", WasteWater);

            FoodConsumptionRate = Utilities.GetValue(config, "FoodConsumptionRate", FoodConsumptionRate);
            WaterConsumptionRate = Utilities.GetValue(config, "WaterConsumptionRate", WaterConsumptionRate);
            OxygenConsumptionRate = Utilities.GetValue(config, "OxygenConsumptionRate", OxygenConsumptionRate);
            ElectricityConsumptionRate = Utilities.GetValue(config, "ElectricityConsumptionRate", ElectricityConsumptionRate);
            BaseElectricityConsumptionRate = Utilities.GetValue(config, "BaseElectricityConsumptionRate", BaseElectricityConsumptionRate);
            EvaElectricityConsumptionRate = Utilities.GetValue(config, "EvaElectricityConsumptionRate", EvaElectricityConsumptionRate);
            CO2ProductionRate = Utilities.GetValue(config, "CO2ProductionRate", CO2ProductionRate);
            WasteProductionRate = Utilities.GetValue(config, "WasteProductionRate", WasteProductionRate);
            WasteWaterProductionRate = Utilities.GetValue(config, "WasteWaterProductionRate", WasteWaterProductionRate);

            MaxTimeWithoutFood = Utilities.GetValue(config, "MaxTimeWithoutFood", MaxTimeWithoutFood);
            MaxTimeWithoutWater = Utilities.GetValue(config, "MaxTimeWithoutWater", MaxTimeWithoutWater);
            MaxTimeWithoutOxygen = Utilities.GetValue(config, "MaxTimeWithoutOxygen", MaxTimeWithoutOxygen);
            MaxTimeWithoutElectricity = Utilities.GetValue(config, "MaxTimeWithoutElectricity", MaxTimeWithoutElectricity);

            DefaultResourceAmount = Utilities.GetValue(config, "DefaultResourceAmount", DefaultResourceAmount);
            EvaDefaultResourceAmount = Utilities.GetValue(config, "EvaDefaultResourceAmount", EvaDefaultResourceAmount);

            RespawnDelay = Utilities.GetValue(config, "RespawnDelay", RespawnDelay);
        }

        public void Save(ConfigNode config)
        {
            config.AddValue("Food", Food);
            config.AddValue("Water", Water);
            config.AddValue("Oxygen", Oxygen);
            config.AddValue("CO2", CO2);
            config.AddValue("Waste", Waste);
            config.AddValue("WasteWater", WasteWater);

            config.AddValue("FoodConsumptionRate", FoodConsumptionRate);
            config.AddValue("WaterConsumptionRate", WaterConsumptionRate);
            config.AddValue("OxygenConsumptionRate", OxygenConsumptionRate);
            config.AddValue("ElectricityConsumptionRate", ElectricityConsumptionRate);
            config.AddValue("BaseElectricityConsumptionRate", BaseElectricityConsumptionRate);
            config.AddValue("EvaElectricityConsumptionRate", EvaElectricityConsumptionRate);
            config.AddValue("CO2ProductionRate", CO2ProductionRate);
            config.AddValue("WasteProductionRate", WasteProductionRate);
            config.AddValue("WasteWaterProductionRate", WasteWaterProductionRate);

            config.AddValue("MaxTimeWithoutFood", MaxTimeWithoutFood);
            config.AddValue("MaxTimeWithoutWater", MaxTimeWithoutWater);
            config.AddValue("MaxTimeWithoutOxygen", MaxTimeWithoutOxygen);
            config.AddValue("MaxTimeWithoutElectricity", MaxTimeWithoutElectricity);

            config.AddValue("DefaultResourceAmount", DefaultResourceAmount);
            config.AddValue("EvaDefaultResourceAmount", EvaDefaultResourceAmount);

            config.AddValue("RespawnDelay", RespawnDelay);
        }
    }
}
