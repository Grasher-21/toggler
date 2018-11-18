using System.Collections.Generic;
using TogglerAPI.Models;

namespace TogglerAPI.Interfaces
{
    public interface IToggleRepository
    {
        int CreateToggle(string name, bool value);
        bool UpdateToggle(int id, string name, bool value);
        bool DeleteToggle(int id);
        Toggle GetToggle(int id);
        List<Toggle> GetToggleList();
    }
}
