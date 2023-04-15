using Microsoft.EntityFrameworkCore.Query.Internal;
using proyectoprogra6_api.Models;

namespace proyectoprogra6_api.ModelsDTOs
{
    public class ItemDTO
    {

        public ItemDTO(Item item)
        {
            ItemId = item.ItemId;
            ItemName = item.ItemName;
            ItemDescription = item.ItemDescription;
            Code = item.Code;
            Active = item.Active;
        }

        public ItemDTO(int itemId, string itemName, string itemDescription, string code, bool? active)
        {
            ItemId = itemId;
            ItemName = itemName;
            ItemDescription = itemDescription;
            Code = code;
            Active = active;
        }

        public ItemDTO()
        {
            
        }

        //return the original model
        public Item getNativeModel()
        {
            Item model = new Item();
            model.ItemId = ItemId;
            model.ItemName = ItemName;
            model.ItemDescription = ItemDescription;
            model.Code = Code;
            model.Active = Active;
            return model;
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool? Active { get; set; }


    }
}
