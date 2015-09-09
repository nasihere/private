package com.shifa.kent.chatsdk;

import android.database.Cursor;
import android.util.Log;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;

public class ChatMain {
    public String _id;
    public int expert = 0;
    public String chatter_to = "";
    public String .XXXXXXX_to = "";
    String chat = "";
    String frm = "";
    Date dDateTime;
    String paiduser;
    String datetime;
    String chatter;
    int cnt = 0;
    SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss.SSS");
    Calendar currentdatetime = Calendar.getInstance();
    String Doctor = "N";
    String id_app;
    String name_member;
    String chat_type = "";
    String id_web;
    String location;
    String picture = "";
    String _to;
    String base_chat;
    int iRead = 1;
    String info;

    public ChatMain(Cursor cursor, String DataFor) {

        try {
            if (DataFor.equals("PrivateChatterNames")) {
                //PrivateMsgRecentList(cursor);
            } else if (DataFor.equals("ShifaExpertMembers")) {
                ShifaMemberList(cursor);
            } else if (DataFor.equals("PrivateMessageChatting")) {
                //PrivateMessageChatting(cursor);
            } else if (DataFor.equals("DiscussionChat")) {
                DiscussionRecentList(cursor);
            }

        } catch (Exception ex) {
            Log.e("ChatUI", "Error in Cols Parsing..." + ex.toString());
        }
    }

    public ChatMain(String chat, String frm, String chatter, String _id, String datetime, String id_web, String id_app, String Picture, String chat_type) {
        super();
        //  String pic1 = "c9e765f0-0a33-46ef-8683-1704d8710a71_thumb.jpg";
        //   String pic2 = "2cff1eb7-b119-43cb-927b-dc93ae57181b";
        //    Picture = "chat_img/thumb/" + pic2;

        //Log.e("CHATUI","ChatMain add start");
        this._id = _id;
        this.datetime = datetime;
        this.id_app = id_app;
        //Log.e("CHATUI","ChatMain add idapp");
        if (Picture != null && !Picture.equals("null")) {
            this.picture = Picture;
        }
        this.id_web = id_web;

        //Log.e("CHATUI","ChatMain add idweb");
        if (chat != null && !chat.equals(""))
            this.chat = chat + "<br><small><font color='" + getRandomColor() + "'>" + chatter + "</font> <font size='2' color='#B0B0B0'>" + datetime + "</font></small>";
        this.frm = frm;
        //this.chat += " == " +  this.id_web + " == " + this.chat_type;


        this.chatter = GetCursorVal(chatter);
        this.chat_type = GetCursorVal(chat_type);
        //   this.chat += this.id_web + " == "+ this.picture;
        //    Log.e("CHATUI","ChatMain add");
    }

    public static String getRandomColor() {
        String[] letters = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"};
        String color = "#";
        for (int i = 0; i < 6; i++) {
            color += letters[(int) Math.round(Math.random() * 15)];
        }
        return color;
    }

    public String getchatter() {
        return chatter;
    }

    public void setchatter(String chatter) {
        this.chatter = chatter;
    }

    public String getChat() {
        return chat;
    }

    public void setChat(String chat) {
        this.chat = chat;
    }

    public String getFrm() {
        return frm;
    }

    public void setFrm(String frm) {
        this.frm = frm;
    }

    private String GetCursorVal(String Val) {

        if (Val == null) return "";
        return Val;
    }

    public void ShifaMemberList(Cursor cursor) {
        try {
            this.chatter = GetCursorVal(cursor.getString(cursor.getColumnIndex("name_member")));
            this.datetime = getDate(GetCursorVal(cursor.getString(cursor.getColumnIndex("datetime"))));
            this.frm = GetCursorVal(cursor.getString(cursor.getColumnIndex(".XXXXXXX")));
            this.location = GetCursorVal(cursor.getString(cursor.getColumnIndex("location")));
            this.chat = GetCursorVal(cursor.getString(cursor.getColumnIndex("info")));
            this._to = "-666";
            this.chat_type = "Friends";
            this.paiduser = GetCursorVal(cursor.getString(cursor.getColumnIndex("paiduser")));
            if (this.location != null && this.location.equals(", ")) this.location = "";
            if (this.chat != null) {
                if (!this.chat.equals("")) {
                    if (this.location != null && !this.location.equals("")) {
                        this.chat = this.chat + " - " + this.location;
                    }
                }

            }
            if (cursor.getString(cursor.getColumnIndex("expert")) != null) {
                this.expert = Integer.valueOf(GetCursorVal(cursor.getString(cursor.getColumnIndex("expert"))));
            }
            //  this.chat += " ==== " + this.id_web;// +  " == "+ this..XXXXXXX_to;

        } catch (Exception ex) {
            Log.e("ChatUI", "error in shifa member list " + ex.toString() + " item : " + this.chatter + ", " + this.datetime + ", " + this.frm + ", " + this.location + ", " + this.chat + ", " + this._to + ", "
                    + this.chat_type + ", " + this.expert + ", ");
        }

    }

    public void DiscussionRecentList(Cursor cursor) {
        this.chat = cursor.getString(cursor.getColumnIndex("chat"));
        if (this.chat != null && !this.chat.equals("")) {
            if (this.chat.length() >= 36) {
                this.chat = this.chat.substring(0, 35) + "...";
            }
        }
        //   Log.e("Test","test2");

        this.id_web = cursor.getString(cursor.getColumnIndex("id_web"));
        this.chatter = cursor.getString(cursor.getColumnIndex("chatter"));
        //    this.chatter += " ==== " + this.id_web;// +  " == "+ this..XXXXXXX_to;
        this.datetime = getDate(cursor.getString(cursor.getColumnIndex("datetime")));
        try {
            // Date dt = sdf.parse(this.datetime);
            //  this.datetime = dt.getDate().to;
        } catch (Exception ex) {

        }
        // this.chat += " "+ this.id_web;
        if (cursor.getString(cursor.getColumnIndex("iRead")) != null) {
            this.iRead = Integer.valueOf(cursor.getString(cursor.getColumnIndex("iRead")));
        }

        this..XXXXXXX_to = cursor.getString(cursor.getColumnIndex(".XXXXXXX_to"));
        this.chatter_to = cursor.getString(cursor.getColumnIndex("chatter_to"));
        this.frm = cursor.getString(cursor.getColumnIndex("frm"));
        this.chat_type = cursor.getString(cursor.getColumnIndex("chat_type"));

        this._to = GetCursorVal(cursor.getString(cursor.getColumnIndex("_to")));
        this.chat_type = GetCursorVal(cursor.getString(cursor.getColumnIndex("chat_type")));
        this.base_chat = cursor.getString(cursor.getColumnIndex("base_chat"));
        if (this.base_chat != null && !this.base_chat.equals("")) {
            if (this.base_chat.length() >= 21) {
                this.chatter += " @ " + this.base_chat.substring(0, 15) + "...";
            } else {
                this.chatter += " @ " + this.base_chat;

            }
        }
        //  this.chat = this.chat + "<br><small><font color='"+getRandomColor()+"'>" + this.chatter+ "</font> <font size='2' color='#B0B0B0'>"  + this.datetime+ "</font></small>" ;

    }

    public void PrivateMsgRecentList(Cursor cursor) {
        this.chat = cursor.getString(cursor.getColumnIndex("chat"));
        if (this.chat != null && !this.chat.equals("")) {
            if (this.chat.length() >= 78) {
                this.chat = this.chat.substring(0, 75) + "...";
            }
        }
        this.chatter = cursor.getString(cursor.getColumnIndex("chatter"));
        this.datetime = cursor.getString(cursor.getColumnIndex("datetime"));
        this.id_web = cursor.getString(cursor.getColumnIndex("id_web"));
        // this.chat += " "+ this.id_web;
        if (cursor.getString(cursor.getColumnIndex("iRead")) != null) {
            this.iRead = Integer.valueOf(cursor.getString(cursor.getColumnIndex("iRead")));
        }
        this..XXXXXXX_to = cursor.getString(cursor.getColumnIndex(".XXXXXXX_to"));
        this.chatter_to = cursor.getString(cursor.getColumnIndex("chatter_to"));
        this.frm = cursor.getString(cursor.getColumnIndex("frm"));

        //  this.chat = this.chat + "<br><small><font color='"+getRandomColor()+"'>" + this.chatter+ "</font> <font size='2' color='#B0B0B0'>"  + this.datetime+ "</font></small>" ;

    }

    public void PrivateMessageChatting(Cursor cursor) {
        /*this.chat = cursor.getString(cursor.getColumnIndex("chat"));

        this.chatter = cursor.getString(cursor.getColumnIndex("chatter"));
        this.datetime = cursor.getString(cursor.getColumnIndex("datetime"));
        this.id_web  = cursor.getString(cursor.getColumnIndex("id_web"));
        this._id  = cursor.getString(cursor.getColumnIndex("id"));
        this..XXXXXXX_to = cursor.getString(cursor.getColumnIndex(".XXXXXXX_to"));
        this.chatter_to = cursor.getString(cursor.getColumnIndex("chatter_to"));
        this.frm = cursor.getString(cursor.getColumnIndex("frm"));
        this.picture = cursor.getString(cursor.getColumnIndex("picture"));
        if (chat != null && !chat.equals(""))
            this.chat = chat + "<br><small><font color='"+getRandomColor()+"'>" + chatter+ "</font> <font size='2' color='#B0B0B0'>"  + datetime+ "</font></small>" ;
//        this.chat += " "+ this.id_web;

*/
    }

    private String getDate(String dateString) {
        if (dateString.equals("")) return "";
        //  Log.e("Datetime dateString",dateString.toString());
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:s");
        formatter.setTimeZone(TimeZone.getTimeZone("America/Los_Angeles"));
        Date value = null;
        try {
            value = formatter.parse(dateString);
            // Log.e("Datetime value",value.toString());
        } catch (ParseException e) {
            e.printStackTrace();
        }
        SimpleDateFormat dateFormatter = new SimpleDateFormat("yyyy-MM-dd hh:mm a");
        dateFormatter.setTimeZone(TimeZone.getDefault());
        // dateFormatter.setTimeZone(TimeZone.getTimeZone("Asia/Calcutta"));
        String dt = dateFormatter.format(value);
        //Log.e("DateTime dt",dt);
        String human = getTimeDiff(dt);
        // Log.e("DateTime human",human+"");

        return human;
        // return dt;
    }

    public String getTimeDiff(String TimeZoneDTTime) {
        Calendar c = Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
        String strDate = sdf.format(c.getTime());
        // Log.e("DateTime TimeZoneDTTime.substring(0, 10) ",TimeZoneDTTime.substring(0, 10)+"");
        //  Log.e("DateTime strDate ",strDate+"");
        if (TimeZoneDTTime.substring(0, 10).equals(strDate)) {
            return "<font color='#3E80B4'>" + TimeZoneDTTime.substring(11) + "</font>";
        } else {
            return TimeZoneDTTime.substring(0, 10);
        }
        // return new Date(secondsTimeDiff).toString();
        //return TimeZoneDTTime;
    }

}