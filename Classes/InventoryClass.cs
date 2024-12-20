﻿using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WashablesSystem.Classes
{
    internal class InventoryClass
    {
        private string itemID;
        private string itemName;
        private string itemCategory;
        private decimal itemQuantity;
        private string itemUnit;
        private decimal itemPrice;
        private bool availabilityStatus;

        SessionVariables sessionVar = new SessionVariables();
        private SqlConnection constring;

        public InventoryClass()
        {
            constring = sessionVar.Constring;
        }

        public InventoryClass(string ItemName, string Category, decimal Quantity, decimal Price, string Unit)
        {
            constring = sessionVar.Constring;
            this.itemName = ItemName;
            this.itemCategory = Category;   
            this.itemQuantity = Quantity;   
            this.itemPrice = Price;
            this.itemUnit = Unit;
        }

        public void addItem()
        {
            constring.Open();

            //Verify if item name exists
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [Item] WHERE item_name ='" + itemName + "'", constring);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "0")
            {
                //Add Item to Database
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 [item_id] FROM [Item] ORDER BY [item_id] DESC", constring);
                SqlDataReader reader1;
                reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    itemID = reader1.GetString(0);
                    int num = int.Parse(string.Join("", itemID.Where(Char.IsDigit))) + 1;
                    itemID = "ITM" + num;
                }
                else
                {
                    itemID = "ITM1";
                }
                reader1.Close();
                cmd.Dispose();

                //Query for inserting
                string query = "INSERT INTO [Item] VALUES('" + itemID + "','" + itemName + "','"
                                 + itemCategory + "','" + itemQuantity + "','" + itemPrice + "',0,'" + itemUnit + "');";

                SqlCommand cmd2 = new SqlCommand(query, constring);
                cmd2.CommandText = query;

                //If successful, add to activity log
                if (cmd2.ExecuteNonQuery() == 1)
                {
                    constring.Close();
                    logOperation("Added New Item");

                }
                else
                {
                    MessageBox.Show("Something went wrong. Please try again.");
                    constring.Close();
                }
            }
            else
            {
                MessageBox.Show("Item name already exists!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                constring.Close();
            }
        }
        public void editItem(string itemID)
        {
            constring.Open();

            this.itemID = itemID;

            //Query for editing
            String query = "UPDATE [Item] SET item_name='"
                + itemName + "',item_category='" + itemCategory + "',item_quantity='" + itemQuantity
                + "',item_price='" + itemPrice + "',item_measurement='"
                + itemUnit + "' WHERE item_id='" + itemID + "'";

            SqlCommand cmd2 = new SqlCommand(query, constring);
            cmd2.CommandText = query;

            //If successful, add to activity log
            if (cmd2.ExecuteNonQuery() == 1)
            {
                constring.Close();
                logOperation("Edited Item");
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again.");
                constring.Close();
            }
        }
        public DataTable displaySelectedItem(string itemID)
        {
            constring.Open();
            string sql = "SELECT * FROM [Item] WHERE item_id = '" + itemID + "'";
            DataTable itemInfo = new DataTable("itemInfo");
            SqlDataAdapter da = new SqlDataAdapter(sql, constring);
            da.Fill(itemInfo);
            constring.Close();

            return itemInfo;
        }
        public void restoreItem(string itemID)
        {
            constring.Open();

            this.itemID = itemID;

            String query = "UPDATE [Item] SET archived=0 WHERE item_id='" + itemID + "';";

            SqlCommand cmd2 = new SqlCommand(query, constring);
            cmd2.CommandText = query;

            //If successful, add to activity log
            if (cmd2.ExecuteNonQuery() == 1)
            {
                constring.Close();
                logOperation("Restored Item");
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again.");
                constring.Close();
            }
        }
        public void restockItem(string itemID)
        {

        }
        public void archiveItem(string itemID)
        {
            constring.Open();

            this.itemID = itemID;

            String query = "UPDATE [Item] SET archived=1 WHERE item_id='" + itemID + "';";

            SqlCommand cmd2 = new SqlCommand(query, constring);
            cmd2.CommandText = query;

            //If successful, add to activity log
            if (cmd2.ExecuteNonQuery() == 1)
            {
                constring.Close();
                logOperation("Restored Item");
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again.");
                constring.Close();
            }
        }
        public void deleteItem(string itemID)
        {
            constring.Open();

            this.itemID = itemID;

            String query = "DELETE FROM [Item] WHERE item_id='" + itemID + "';";

            SqlCommand cmd2 = new SqlCommand(query, constring);
            cmd2.CommandText = query;

            //If successful, add to activity log
            if (cmd2.ExecuteNonQuery() == 1)
            {
                constring.Close();
                logOperation("Deleted Item");
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again.");
                constring.Close();
            }
        }
        public DataTable displayItem()
        {
            constring.Open();
            string sql = "SELECT * FROM [Item] WHERE archived = 0";
            DataTable item = new DataTable("item");
            SqlDataAdapter da = new SqlDataAdapter(sql, constring);
            da.Fill(item);
            constring.Close();

            return item;
        }
        public DataTable displayItemArchive()
        {
            constring.Open();
            string sql = "SELECT * FROM [Item] WHERE archived = 1";
            DataTable itemInfo = new DataTable("itemInfo");
            SqlDataAdapter da = new SqlDataAdapter(sql, constring);
            da.Fill(itemInfo);
            constring.Close();

            return itemInfo;
        }
        public DataTable displayItemHistory(string itemID)
        {
            constring.Open();
            string sql = "SELECT * FROM [ItemHistory] INNER JOIN [User] ON [ItemHistory].[user_id] = [User].[user_id] WHERE [item_id] = '" + itemID + "'";
            DataTable itemHistory = new DataTable("item");
            SqlDataAdapter da = new SqlDataAdapter(sql, constring);
            da.Fill(itemHistory);
            constring.Close();

            return itemHistory;
        }
        private void logOperation(string activity)
        {
            constring.Open();
            int logID = 0;
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 [log_id] FROM ActivityLog ORDER BY [log_id] DESC", constring);
            SqlDataReader reader1;
            reader1 = cmd.ExecuteReader();
            if (reader1.Read())
            {
                logID = reader1.GetInt32(0) + 1;
            }
            else
            {
                logID = 1;
            }
            reader1.Close();
            cmd.Dispose();

            if (activity.Equals("Added New Item"))
            {
                string queryAct = "INSERT INTO ActivityLog VALUES('" + logID + "','" + sessionVar.loggedIn.ToString() + "','added item "
             + itemID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Inventory" + "')";
                SqlCommand cmdAct = new SqlCommand(queryAct, constring);
                cmdAct.CommandText = queryAct;
                cmdAct.ExecuteNonQuery();
                MessageBox.Show("Item successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                constring.Close();
            }

            else if (activity.Equals("Edited Item"))
            {
                string queryAct = "INSERT INTO ActivityLog VALUES('" + logID + "','" + sessionVar.loggedIn.ToString() + "','edited item "
                            + itemID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Inventory" + "')";
                SqlCommand cmdAct = new SqlCommand(queryAct, constring);
                cmdAct.CommandText = queryAct;
                cmdAct.ExecuteNonQuery();
                MessageBox.Show("Edit successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                constring.Close();
            }
            else if (activity.Equals("Archived Item"))
            {
                string queryAct = "INSERT INTO ActivityLog VALUES('" + logID + "','" + sessionVar.loggedIn.ToString() + "','archived item "
                            + itemID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Inventory" + "')";
                SqlCommand cmdAct = new SqlCommand(queryAct, constring);
                cmdAct.CommandText = queryAct;
                cmdAct.ExecuteNonQuery();
                MessageBox.Show("Item successfully archived!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                constring.Close();
            }
            else if (activity.Equals("Restocked Item"))
            {
                string queryAct = "INSERT INTO ActivityLog VALUES('" + logID + "','" + sessionVar.loggedIn.ToString() + "','restocked item"
                            + itemID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Inventory" + "')";
                SqlCommand cmdAct = new SqlCommand(queryAct, constring);
                cmdAct.CommandText = queryAct;
                cmdAct.ExecuteNonQuery();

                string queryAddHistory = "INSERT INTO ItemHistory VALUES('" + sessionVar.loggedIn.ToString() + "','" + itemID +  "','" 
                    + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + itemQuantity + itemUnit + "')";
                SqlCommand cmdAddHistory = new SqlCommand(queryAddHistory, constring);
                cmdAddHistory.CommandText = queryAddHistory;
                cmdAddHistory.ExecuteNonQuery();
                MessageBox.Show("Item successfully restocked!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                constring.Close();
            }
            else if (activity.Equals("Restored Item"))
            {
                string queryAct = "INSERT INTO ActivityLog VALUES('" + logID + "','" + sessionVar.loggedIn.ToString() + "','restored item "
                            + itemID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Inventory" + "')";
                SqlCommand cmdAct = new SqlCommand(queryAct, constring);
                cmdAct.CommandText = queryAct;
                cmdAct.ExecuteNonQuery();
                MessageBox.Show("Item successfully restored!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                constring.Close();
            }
            else if (activity.Equals("Deleted Item"))
            {
                string queryAct = "INSERT INTO ActivityLog VALUES('" + logID + "','" + sessionVar.loggedIn.ToString() + "','deleted item "
                            + itemID + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','Inventory" + "')";
                SqlCommand cmdAct = new SqlCommand(queryAct, constring);
                cmdAct.CommandText = queryAct;
                cmdAct.ExecuteNonQuery();
                MessageBox.Show("Item deleted permanently!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                constring.Close();
            }
        }
    }
}
