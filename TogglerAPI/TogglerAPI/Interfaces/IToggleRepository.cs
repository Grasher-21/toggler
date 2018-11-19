using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IToggleRepository
    {
        int CreateToggle(ToggleModel toggleModel);
        bool DeleteToggle(int id);
        ToggleModel GetToggle(int id);
        List<ToggleModel> GetToggleList();
        List<ToggleModel> GetToggleListByIds(List<int> idList);
        bool UpdateToggle(ToggleModel toggleModel);
    }
}
