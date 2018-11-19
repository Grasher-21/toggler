using System;
using System.Collections.Generic;
using System.Linq;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Repositories
{
    public class ToggleServicePermissionRepository : IToggleServicePermissionRepository
    {
        private readonly TogglerContext TogglerContext;
        private readonly ILogger Logger;
        private readonly IToggleRepository ToggleRepository;
        private readonly IServiceRepository ServiceRepository;

        public ToggleServicePermissionRepository(TogglerContext togglerContext, ILogger logger, IToggleRepository toggleRepository, IServiceRepository serviceRepository)
        {
            TogglerContext = togglerContext;
            Logger = logger;
            ToggleRepository = toggleRepository;
            ServiceRepository = serviceRepository;
        }

        public bool CreatePermission(ToggleServicePermissionModel toggleServicePermissionModel)
        {
            if (toggleServicePermissionModel == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                ToggleModel toggleModel = ToggleRepository.GetToggle(toggleServicePermissionModel.ToggleId);
                ServiceModel serviceModel = ServiceRepository.GetService(toggleServicePermissionModel.ServiceId);

                if (toggleModel != null && serviceModel != null)
                {
                    TogglerContext.ToggleServicePermissions.Add(toggleServicePermissionModel);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to create the ToggleServicePermission: {ex.Message}");
            }

            return false;
        }

        public bool DeletePermission(int toggleId, Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                ToggleServicePermissionModel toggleServicePermission = TogglerContext.ToggleServicePermissions
                    .Where(p => p.ToggleId == toggleId && p.ServiceId == serviceId).FirstOrDefault();

                if (toggleServicePermission != null)
                {
                    TogglerContext.ToggleServicePermissions.Attach(toggleServicePermission);
                    TogglerContext.ToggleServicePermissions.Remove(toggleServicePermission);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to delete the ToggleServicePermission: {ex.Message}");
            }

            return false;
        }

        public ToggleServicePermissionModel GetToggleServicePermission(int toggleId, Guid serviceId)
        {
            if (serviceId == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                return TogglerContext.ToggleServicePermissions.Where(p => p.ToggleId == toggleId && p.ServiceId == serviceId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the ToggleServicePermission with toggleId = {toggleId} and serviceId = {serviceId}: {ex.Message}");

                return null;
            }
        }

        public List<ToggleServicePermissionModel> GetToggleServicePermissionList()
        {
            try
            {
                return TogglerContext.ToggleServicePermissions.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to get the ToggleServicePermission list: {ex.Message}");

                return null;
            }
        }

        public bool UpdatePermission(ToggleServicePermissionModel toggleServicePermissionModel)
        {
            if (toggleServicePermissionModel == null || toggleServicePermissionModel.ServiceId == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            try
            {
                ToggleServicePermissionModel toggleService = TogglerContext.ToggleServicePermissions
                    .Find(toggleServicePermissionModel.ToggleId, toggleServicePermissionModel.ServiceId);

                if (toggleService != null)
                {
                    toggleService.State = toggleServicePermissionModel.State;
                    toggleService.OverridenValue = toggleServicePermissionModel.OverridenValue;

                    TogglerContext.ToggleServicePermissions.Update(toggleService);
                    TogglerContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile($"Failed to update the ToggleServicePermission: {ex.Message}");
            }

            return false;
        }
    }
}
