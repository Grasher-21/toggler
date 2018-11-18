using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.Utilities;

namespace TogglerAPI.Repositories
{
    public class ToggleRepository : IToggleRepository
    {
        private readonly TogglerContext TogglerContext;

        public ToggleRepository(TogglerContext togglerContext)
        {
            TogglerContext = togglerContext;
        }

        public int CreateToggle(string name, bool value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            ToggleModel Toggle = new ToggleModel() { Name = name, Value = value };

            TogglerContext.Toggles.Add(Toggle);
            TogglerContext.SaveChanges();

            return Toggle.ToggleId;
        }

        public bool DeleteToggle(int id)
        {
            try
            {
                ToggleModel Toggle = new ToggleModel() { ToggleId = id };

                TogglerContext.Toggles.Attach(Toggle);
                TogglerContext.Toggles.Remove(Toggle);
                TogglerContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to delete the Toggle: {ex.Message}");

                return false;
            }
        }

        public ToggleModel GetToggle(int id)
        {
            return TogglerContext.Toggles.Find(id);
        }

        public List<ToggleModel> GetToggleList()
        {
            return TogglerContext.Toggles.ToList();
        }

        public bool UpdateToggle(int id, string name, bool value)
        {
            ToggleModel toggle = TogglerContext.Toggles.Find(id);

            try
            {
                if (toggle != null)
                {
                    toggle.Name = name;
                    toggle.Value = value;

                    TogglerContext.Toggles.Update(toggle);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggerFile.LogFile($"Failed to update the Toggle: {ex.Message}");
            }

            return false;
        }
    }
}
