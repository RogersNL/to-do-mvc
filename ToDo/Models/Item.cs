using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDo.Models
{
  public class Item
  {
    private string _description;
    private int _id;
    // private static List<Item> _instances = new List<Item> {};
    public Item(string Description, int Id = 0)
    {
      _id = Id;
      _description = Description;
    }


    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        return (idEquality && descriptionEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `items` (`description`) VALUES (@ItemDescription);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;    // This line is new!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    // public Item (string description)
    // {
    //   _description = description;
    //   _instances.Add(this);
    //   _id = _instances.Count;
    // }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public int GetId()
    {
      return _id;
    }
    public static List<Item> GetAll()
    {
      // return _instances;
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    // public static Item Find(int id)
    // {
    //   Item foundItem= new Item("testDescription");
    //   return foundItem;
    // }
    public static Item Find(int id)
     {
         MySqlConnection conn = DB.Connection();
         conn.Open();
         var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM 'items' WHERE id = @thisId;";
         // more logic will go here!
         MySqlParameter thisId = new MySqlParameter();
           thisId.ParameterName = "@thisId";
           thisId.Value = id;
           cmd.Parameters.Add(thisId);
           var rdr = cmd.ExecuteReader() as MySqlDataReader;
           int itemId = 0;
           string itemDescription = "";

           while (rdr.Read())
           {
               itemId = rdr.GetInt32(0);
               itemDescription = rdr.GetString(1);
           }
           Item foundItem= new Item(itemDescription, itemId);

          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
            }
            return foundItem;
     }

    // public void Save()
    // {
    //   _instances.Add(this);
    // }
    // public static void ClearAll()
    // {
    //   _instances.Clear();
    // }
    // public static Item Find(int searchId)
    // {
    //   return _instances[searchId-1];
    // }
  }
}
