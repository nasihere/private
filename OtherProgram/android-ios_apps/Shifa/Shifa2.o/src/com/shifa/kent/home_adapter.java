package com.shifa.kent;

import android.content.Context;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

public class home_adapter {

    public static ArrayList<home_modal> Items;

    public static void LoadModel(String ItemStatus, String AdsFreeStatus, String FlashStatus, String NewsStatus, String MyProfileStatus, String ads, String DiscussionStatus, String PrivateStatus, String SessionID) {

        if (PrivateStatus.equals("")) PrivateStatus = "<b>Loading..</b>";
        if (DiscussionStatus.equals("")) DiscussionStatus = "<b>Loading..</b>";
        if (AdsFreeStatus.equals("")) AdsFreeStatus = "";
        if (FlashStatus.equals("")) FlashStatus = "<b>Loading..</b>";
        if (NewsStatus.equals("")) NewsStatus = "<b>Loading..</b>";

        int index = 1;
        //15 no is highest
        Items = new ArrayList<home_modal>();
       // Items.add(new home_modal(1, "22", "WeCure Doctor", "Extra Charge"));
        Items.add(new home_modal(index++, "20", "Facebook Login Connect", SessionID));
        Items.add(new home_modal(index++, "1", "Kent Repertory", ItemStatus));
        Items.add(new home_modal(index++, "18", "Boenninghausen Repertory", ItemStatus));
        Items.add(new home_modal(index++, "19", "Cyrus Maxwell Boger Repertory", ItemStatus));

        Items.add(new home_modal(index++, "14", "Patient Management", ItemStatus));
        Items.add(new home_modal(index++, "3", "Reversed Repertory", ItemStatus));
        Items.add(new home_modal(index++, "2", "Materia Medica", ItemStatus));
        Items.add(new home_modal(index++, "4", "Abbreviation", ItemStatus));
        Items.add(new home_modal(index++, "6", "Organon", ItemStatus));
        Items.add(new home_modal(index++, "5", "Chat", ItemStatus));
        if (!ads.equalsIgnoreCase("true")) {
            Items.add(new home_modal(index++, "7", "Buy Now", ""));
        } else {
            Items.add(new home_modal(index++, "", "", ""));
        }
        Items.add(new home_modal(index++, "17", "Settings", ItemStatus));


        int talka = (int) (Math.random() * 2); // between 0 and 1
        if (talka == 0) {
            Items.add(new home_modal(index++, "21", "www.shifa.in", ItemStatus));
        } else {
            Items.add(new home_modal(index++, "21", "Shifa Facebook Page", ItemStatus));

        }

    }



    public static home_modal GetbyId(int id) {

        for (home_modal item : Items) {
            if (item.Id == id) {
                return item;
            }
        }
        return null;
    }

    private static Date getStringToDate(String str){
        String dtStart = str;
        Date date;
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        try {
            date = format.parse(dtStart);
            return date;
        } catch (Exception e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return null;
        }

    }
    private static String getDateToShort(Date date){
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        try {
            String dt = format.format(date);
            return dt.toString();
        } catch (Exception e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return null;
        }

    }

    /*private static String getStatusCure(Date compare){

        Date DateExpiryDietNutri =  getStringToDate(checkdt);
        if (DateExpiryDietNutri != null) {
            Date date = new Date();
            boolean isExpired = date.after(DateExpiryDietNutri);
        }
    }*/
    public static ArrayList<home_modal> LoadCureModel(Context ctx) {

        Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
        Items = new ArrayList<home_modal>();
        Date date = new Date();
        int index = 1;
        String LabelShow = "Dietitians and Nutritionists";
        String checkdt = SLAc.GetPreferenceValue("PaidServiceExpiry"+LabelShow);
        if (checkdt.equals("0")) {
            Items.add(new home_modal(index++, "91", LabelShow, "Pay Now"));
        }
        else{
            Date DateExpiryDietNutri =  getStringToDate(checkdt);
            if (DateExpiryDietNutri != null) {

                boolean isExpired = date.after(DateExpiryDietNutri);
                if (isExpired == false) {
                    Items.add(new home_modal(index++, "91", LabelShow, "<font color='green'>Expiry</font><br />" + getDateToShort(DateExpiryDietNutri)));
                }
                else{
                    Items.add(new home_modal(index++, "91", LabelShow, "<font color='red'>Expired</font><br /> Renew it"));

                }
            }

        }



         LabelShow = "Sexual Health";
         checkdt = SLAc.GetPreferenceValue("PaidServiceExpiry"+LabelShow);
        if (checkdt.equals("0")) {
            Items.add(new home_modal(index++, "92", LabelShow, "Pay Now"));
        }
        else{
            Date DateExpiryDietNutri =  getStringToDate(checkdt);
            if (DateExpiryDietNutri != null) {

                boolean isExpired = date.after(DateExpiryDietNutri);
                if (isExpired == false) {
                    Items.add(new home_modal(index++, "92", LabelShow, "<font color='green'>Expiry</font><br /> " + getDateToShort(DateExpiryDietNutri)));
                }
                else{
                    Items.add(new home_modal(index++, "92", LabelShow, "<font color='red'>Expired</font><br /> Renew it"));

                }
            }

        }



         LabelShow = "Homeopathy";
         checkdt = SLAc.GetPreferenceValue("PaidServiceExpiry"+LabelShow);
        if (checkdt.equals("0")) {
            Items.add(new home_modal(index++, "93", LabelShow, "Pay Now"));
        }
        else{
            Date DateExpiryDietNutri =  getStringToDate(checkdt);
            if (DateExpiryDietNutri != null) {

                boolean isExpired = date.after(DateExpiryDietNutri);
                if (isExpired == false) {
                    Items.add(new home_modal(index++, "93", LabelShow, "<font color='green'>Expiry</font><br /> " + getDateToShort(DateExpiryDietNutri)));
                }
                else{
                    Items.add(new home_modal(index++, "93", LabelShow, "<font color='red'>Expired</font><br /> Renew it"));

                }
            }

        }


         LabelShow = "Paediatric Homeopathy";
         checkdt = SLAc.GetPreferenceValue("PaidServiceExpiry"+LabelShow);
        if (checkdt.equals("0")) {
            Items.add(new home_modal(index++, "94", LabelShow, "Pay Now"));
        }
        else{
            Date DateExpiryDietNutri =  getStringToDate(checkdt);
            if (DateExpiryDietNutri != null) {

                boolean isExpired = date.after(DateExpiryDietNutri);
                if (isExpired == false) {
                    Items.add(new home_modal(index++, "94", LabelShow, "<font color='green'>Expiry</font><br /> " + getDateToShort(DateExpiryDietNutri)));
                }
                else{
                    Items.add(new home_modal(index++, "94", LabelShow, "<font color='red'>Expired</font><br /> Renew it"));

                }
            }

        }



        return Items;
    }
}