using System;
using System.Collections.Generic;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.RequestModels;
using TogglerAPI.ResponseModels;

namespace TogglerAPI.BusinessCore
{
    public class ToggleService : IToggleService
    {
        private readonly IToggleRepository ToggleRepository;
        private readonly ILogger Logger;
        private readonly int Invalid = -1;

        public ToggleService(IToggleRepository toggleRepository, ILogger logger)
        {
            ToggleRepository = toggleRepository;
            Logger = logger;
        }

        public int CreateToggle(ToggleRequestModel toggleRequestModel)
        {
            if (toggleRequestModel == null || string.IsNullOrWhiteSpace(toggleRequestModel.Name))
            {
                return Invalid;
            }

            try
            {
                ToggleModel toggleModel = new ToggleModel()
                {
                    Name = toggleRequestModel.Name,
                    Value = toggleRequestModel.Value
                };

                return ToggleRepository.CreateToggle(toggleModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error creating a Toggle: {ex.Message}");

                return Invalid;
            }
        }

        public bool DeleteToggle(int id)
        {
            try
            {
                return ToggleRepository.DeleteToggle(id);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error deleting a Toggle: {ex.Message}");

                return false;
            }
        }

        public ToggleResponseModel GetToggle(int id)
        {
            try
            {
                ToggleModel toggleModel = ToggleRepository.GetToggle(id);

                if (toggleModel != null)
                {
                    ToggleResponseModel toggleResponseModel = new ToggleResponseModel()
                    {
                        ToggleId = toggleModel.ToggleId,
                        Name = toggleModel.Name,
                        Value = toggleModel.Value
                    };

                    return toggleResponseModel;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a Toggle by id = {id}: {ex.Message}");
            }

            return null;
        }

        public List<ToggleResponseModel> GetToggleList()
        {
            try
            {
                List<ToggleModel> toggleModelList = ToggleRepository.GetToggleList();

                if (toggleModelList != null)
                {
                    List<ToggleResponseModel> toggleList = new List<ToggleResponseModel>();

                    foreach (ToggleModel item in toggleModelList)
                    {
                        toggleList.Add(new ToggleResponseModel()
                        {
                            ToggleId = item.ToggleId,
                            Name = item.Name,
                            Value = item.Value
                        });
                    }

                    return toggleList;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error getting a Toggle list: {ex.Message}");
            }

            return null;
        }

        public bool UpdateToggle(ToggleRequestModel toggleRequestModel)
        {
            if (toggleRequestModel?.ToggleId == null || string.IsNullOrWhiteSpace(toggleRequestModel.Name))
            {
                return false;
            }

            try
            {
                ToggleModel toggleModel = new ToggleModel()
                {
                    ToggleId = (int)toggleRequestModel.ToggleId,
                    Name = toggleRequestModel.Name,
                    Value = toggleRequestModel.Value
                };

                return ToggleRepository.UpdateToggle(toggleModel);
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Error updating a Toggle: {ex.Message}");
            }

            return false;
        }
    }
}
