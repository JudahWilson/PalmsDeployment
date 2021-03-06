﻿using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.Services;
using System.Text;
using System.Collections.Generic;

// To allow this Web Service to be called from script, using ASP.NET AJAX, put the following line after
//              [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//>>this here>> [System.Web.Script.Services.ScriptService]
//              public class ID_Card


/// <summary>
/// Summary description for ID_Card
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class IDCard
{
    [WebMethod]
    public char Process_ID_Card(string data)
    {
        // Parse data
        /* TODO */


        // Logic for checking amount of money student has goes here.
        // To simulate, the "check" will randomly pass or fail.
        Random rnd = new Random();
        string status = rnd.Next(100) < 50 ? "Y" : "N"; // 0 <= number < 100

        // Retain invoice
        string query_string = @"DECLARE
                                    v_ NUMBER;
                                BEGIN
                                    v_ = TIA_invoice_package.createInvoice(p_order_id_fk         => :p_order_id_fk,
                                                                            p_confirmation_status => :p_confirmation_status);
                                END;";
        OracleConnection myConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["SEI_DB_Connection"].ConnectionString);
        OracleCommand myCommand = new OracleCommand(query_string, myConnection);

        try
        {
            myConnection.Open();
            myCommand.Parameters.Add("p_order_id_fk",         1);
            myCommand.Parameters.Add("p_confirmation_status", 'Y');
            myCommand.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                myCommand.Dispose();
            }
            catch { }

            myConnection.Close();
            myConnection.Dispose();
        }

        return 'Y';
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "IDCard says Hello World";
    }
}

/// <summary>
/// Summary description for Bump_Board
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Bump_Board
{
    [WebMethod]
    public static char Complete_Order(Int64 orderNumber)
    {
        String query_string = @"BEGIN
                                    updateOrder(p_order_id_pk => :p_order_id_pk
                                                p_order_ready => :p_order_ready);
                                END;";
        OracleConnection myConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["SEI_DB_Connection"].ConnectionString);
        OracleCommand myCommand = new OracleCommand(query_string, myConnection);

        try
        {
            myConnection.Open();
            myCommand.Parameters.Add("p_order_id_pk", orderNumber);
            myCommand.Parameters.Add("p_order_ready", "Y");
            myCommand.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                myCommand.Dispose();
            }
            catch { }

            myConnection.Close();
            myConnection.Dispose();
        }

        return 'Y';
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Bump_Board says Hello World";
    }
}

public static class Manager
{
}

/// <summary>
/// Summary description for Bump_Board
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Database_Queries
{
    public int createCCI(int p_cci_order_id_fk, string p_cci_token, string p_cci_confirmation_status)
    {
        int result;
        String query_string = @"BEGIN :out := createCCI(p_cci_order_id_fk => :p_cci_order_id_fk, p_cci_token => :p_cci_token, p_cci_confirmation_status => :p_cci_confirmation_status, ) END;";
        OracleConnection myConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["SEI_DB_Connection"].ConnectionString);
        OracleCommand myCommand = new OracleCommand(query_string, myConnection);

        try
        {
            myConnection.Open();
            myCommand.Parameters.Add("out", OracleDbType.Int32, ParameterDirection.Output);
            myCommand.Parameters.Add("p_cci_order_id_fk",         p_cci_order_id_fk);
            myCommand.Parameters.Add("p_cci_token",               p_cci_token);
            myCommand.Parameters.Add("p_cci_confirmation_status", p_cci_confirmation_status);
            myCommand.ExecuteNonQuery();

            result = (int)myCommand.Parameters["out"].Value;
        }
        finally
        {
            try
            {
                myCommand.Dispose();
            }
            catch { }

            myConnection.Close();
            myConnection.Dispose();
        }
        return result;
    }

    //createOrder(p_location_id_fk    "order".location_id_fk%TYPE,
    //            p_ticket_id_fk      "order".ticket_id_fk%TYPE,
    //            p_order_num         "order".order_num%TYPE,
    //            p_customer_fname    "order".customer_fname%TYPE,
    //            p_customer_lname    "order".customer_lname%TYPE,
    //            p_order_cal_time    "order".order_cal_time%TYPE,
    //            p_order_cost        "order".order_cost%TYPE,
    //            p_order_ready       "order".order_ready%TYPE,
    //            p_order_placed_date "order".order_placed_date%TYPE)
    //RETURN "order".order_id_pk%TYPE

    //createOrderDetail(p_order_element_id_fk order_detail.order_element_id_fk%TYPE,
    //                  p_detail_id_fk        order_detail.detail_id_fk%TYPE)
    //RETURN order_detail.order_detail_id_pk%TYPE

    //createOrderElement(p_food_id_fk          order_element.food_id_fk%TYPE,
    //                   p_order_id_fk         order_element.order_id_fk%TYPE,
    //                   p_order_element_ready order_element.order_element_ready%TYPE)
    //RETURN order_element.order_element_id_pk%TYPE

    //createPAI(p_order_id_fk         PCC_account_inv.order_id_fk%TYPE,
    //          p_confirmation_status PCC_account_inv.confirmation_status%TYPE)
    //RETURN PCC_account_inv.pai_id_pk%TYPE



    //updatePAI(p_pai_id_pk PCC_account_inv.pai_id_pk%TYPE,
    //deletePAI(p_pai_id_pk PCC_account_inv.pai_id_pk%TYPE);
    //updateOrder(p_order_id_pk       "order".order_id_pk%TYPE,
    //deleteOrder(p_order_id_pk "order".order_id_pk%TYPE);
    //updateOrderElement(p_order_element_id_pk order_element.order_element_id_pk%TYPE,
    //deleteOrderElement(p_order_element_id_pk order_element.order_element_id_pk%TYPE);
    //updateOrderDetail(p_order_detail_id_pk order_detail.order_detail_id_pk%TYPE,
    //deleteOrderDetail(p_order_detail_id_pk order_detail.order_detail_id_pk%TYPE);
    //updateCCI(p_cci_id_pk credit_card_inv.cci_id_pk%TYPE,
    //deleteCCI(p_cci_id_pk credit_card_inv.cci_id_pk%TYPE);


    //createTimeSlot(p_time_slot_date time_slot.time_slot_date%TYPE,
    //updateTimeSlot(p_time_slot_id_pk time_slot.time_slot_id_pk%TYPE,
    //deleteTimeSlot(p_time_slot_id_pk time_slot.time_slot_id_pk%TYPE);
    //createTicket(p_time_slot_id_fk ticket.time_slot_id_fk%TYPE)
    //updateTicket(p_ticket_id_pk ticket.ticket_id_pk%TYPE,
    //deleteTicket(p_ticket_id_pk ticket.ticket_id_pk%TYPE);
    //
    //createLocation(p_location_name  "location".location_name%TYPE,
    //updateLocation(p_location_id_pk "location".location_id_pk%TYPE,
    //deleteLocation(p_location_id_pk "location".location_id_pk%TYPE);
    //createFoodType(p_food_type_name food_type.food_type_name%TYPE,
    //updateFoodType(p_food_type_id_pk food_type.food_type_id_pk%TYPE,
    //deleteFoodType(p_food_type_id_pk food_type.food_type_id_pk%TYPE);
    //createFood(p_food_type_id_fk food.food_type_id_fk%TYPE,
    //updateFood(p_food_id_pk food.food_id_pk%TYPE,
    //deleteFood(p_food_id_pk food.food_id_pk%TYPE);
    //createFoodDetailLine(p_detail_id_fk food_detail_line.detail_id_fk%TYPE,
    //updateFoodDetailLine(p_fdl_id_pk food_detail_line.fdl_id_pk%TYPE,
    //deleteFoodDetailLine(p_fdl_id_pk food_detail_line.fdl_id_pk%TYPE);
    //createDetail(p_detail_descr detail.detail_descr%TYPE,
    //updateDetail(p_detail_id_pk detail.detail_id_pk%TYPE,
    //deleteDetail(p_detail_id_pk detail.detail_id_pk%TYPE);

    [WebMethod]
    public string Menu()
    {
        DataTable menu = new DataTable();
        string query_string = @"SELECT * FROM food";
        OracleConnection myConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["SEI_DB_Connection"].ConnectionString);
        OracleCommand myCommand = new OracleCommand(query_string, myConnection);

        try
        {
            myConnection.Open();
            myCommand.ExecuteReader();

            //...
            //menu.load(...);
        }
        finally
        {
            try
            {
                myCommand.Dispose();
            }
            catch { }

            myConnection.Close();
            myConnection.Dispose();
        }

        StringBuilder sb = new StringBuilder();

        IEnumerable<string> columnNames = menu.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));
        //AppendLine puts "\r\n" between rows?

        foreach (DataRow row in menu.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(",", fields));
        }
        return sb.ToString();
    }

    public void time_slot()
    {
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Database_Queries says Hello World";
    }
}