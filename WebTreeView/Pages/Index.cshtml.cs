
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebTreeView.Models;
using System.Text.Json;

namespace WebTreeView.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public List<Item> Items { get; set; }
       
        public void OnGet()
        {
            LoadJson();
        }

        private void LoadJson()
        {
            using (StreamReader r = new StreamReader("data//Items.json"))
            {
                string json = r.ReadToEnd();
                Items = JsonSerializer.Deserialize<List<Item>>(json);
            }
        }
    }
}