using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Repositories
{
    public class ToggleRepository : IToggleRepository
    {
        private readonly TogglerContext TogglerContext;
        private readonly ILogger Logger;

        public ToggleRepository(TogglerContext togglerContext, ILogger logger)
        {
            TogglerContext = togglerContext;
            Logger = logger;
        }

        public int CreateToggle(ToggleModel toggleModel)
        {
            if (toggleModel == null || string.IsNullOrWhiteSpace(toggleModel.Name))
            {
                throw new ArgumentNullException();
            }

            try
            {
                TogglerContext.Toggles.Add(toggleModel);
                TogglerContext.SaveChanges();

                return toggleModel.ToggleId;
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to create the Toggle: {ex.Message}");

                return -1;
            }
        }

        public bool DeleteToggle(int id)
        {
            try
            {
                ToggleModel toggerModel = TogglerContext.Toggles.Find(id);

                if (toggerModel != null)
                {
                    TogglerContext.Toggles.Attach(toggerModel);
                    TogglerContext.Toggles.Remove(toggerModel);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the Toggle: {ex.Message}");
            }

            return false;
        }

        public ToggleModel GetToggle(int id)
        {
            try
            {
                return TogglerContext.Toggles.Find(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the Toggle with id = {id}: {ex.Message}");

                return null;
            }
        }

        public List<ToggleModel> GetToggleList()
        {
            try
            {
                return TogglerContext.Toggles.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the Toggle list: {ex.Message}");

                return null;
            }
        }

        public bool UpdateToggle(ToggleModel toggleModel)
        {
            if (toggleModel == null || string.IsNullOrWhiteSpace(toggleModel.Name))
            {
                throw new ArgumentNullException();
            }

            try
            {
                ToggleModel toggle = TogglerContext.Toggles.Find(toggleModel.ToggleId);

                if (toggle != null)
                {
                    toggle.Name = toggleModel.Name;
                    toggle.Value = toggleModel.Value;

                    TogglerContext.Toggles.Update(toggle);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to update the Toggle: {ex.Message}");
            }

            return false;
        }
    }
}
