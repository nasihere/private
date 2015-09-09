package shifa.nasz.locally;

import android.content.Context;
import android.database.Cursor;
import android.util.Log;
import android.widget.ListView;

import java.util.ArrayList;


public class DBclass {

	DBHelper db;
	Context ctx;
	Cursor cursor; 
	ArrayList<User> userArray;
	ListView myList;
    String AddNewCaption = "Add New Entry";
    String AddCategory = "Audio Lectures/Bayan, Information, News, Pictures, Business Card, Hotel Menu & Price";
    Super_Library_AppClass SLAc;

    public DBclass()
    {

    }

	public DBclass(Context ctx)
	{
		db = new DBHelper(ctx);



    }

    //Remember Long query never save in one short
    public void KentShifaInData(String jsonValue)
    {
        try{
                String[] qry = jsonValue.split("#_#");
                for(int i =0;i<=qry.length - 1; i++){
                    Log.e("runner",qry[i]);
                    db.getReadableDatabase().execSQL(qry[i]);
                }

        }catch(Exception ex){
            Log.e("runner","error " + ex.toString());
        }
        Log.e("query","Done");

    }
	public void DbQry(String qry)
    {
		Log.e("qry",qry);
		db.getReadableDatabase().execSQL(qry);
		Log.e("qry","Done");

    	   
    }
    private User SendSMSPage(String Category){
        return new User(
                "SMS To Mumbra People",
                "Send SMS to Mumbra People, Share Real Estate/Rental Offers, Update Shop/Classes/Masjid... Sell/Buy your item..",
                "",
                "",
                0,
                0,
                "",
                "",
                "",
                "",
                "",
                "0",
                SLAc.RestoreSessionIndexID("session_id"),""
        );
    }
    private User Header(String Category){
        String ProfileMobile = SLAc.GetPreferenceValue("ProfileMobile");
        String ProfileName = SLAc.GetPreferenceValue("ProfileName") + " - "+ProfileMobile;
        return new User(
                ProfileName,
                "Personal & Secured Account",
                ProfileMobile,
                "",
                0,
                0,
                "",
                "",
                "",
                "",
                "",
                "0",
                SLAc.RestoreSessionIndexID("session_id"),
                ""
        );
    }
    private User AddNewEntry(){
        return new User(
                AddNewCaption,
                AddCategory,
                "",
                "",
                0,
                0,
                "",
                "",
                "",
                "",
                "",
                "0",
                SLAc.RestoreSessionIndexID("session_id"),""
        );
    }
    private ArrayList<User> CreateContactList(){
        ArrayList<User> userArray = new ArrayList<User>();
        String ContactInfo = SLAc.FetchContacts(); // make sure it declare in KentItemListing function
        Log.e("ContactInfo",ContactInfo);
        String[] ContactSplit = ContactInfo.split("#_#");
        if (ContactSplit.length != 0){
            for(int i=0; i <= ContactSplit.length-1; i++){
                String[] NamePhone = ContactSplit[i].split("#-#");
                userArray.add(new User(
                        NamePhone[0],
                        NamePhone[1],
                        "",
                        "",
                        0,
                        0,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "0",
                        SLAc.RestoreSessionIndexID("session_id"),""
                ));
            }
        }
        return userArray;

    }
	public UserCustomAdapter KentItemListing(Context ctx, Cursor cursor, ArrayList<User> userArray, String Category, int sCount)
	{
        SLAc = new Super_Library_AppClass(ctx);
	    userArray.clear();
		cursor.moveToFirst();
        Log.e("KentItemListing Category",Category);
        if (Category.indexOf("|My Contacts") != -1){
            userArray = CreateContactList();
        }
        else if (Category.equals("") || Category.indexOf("|") == -1) {
            userArray.add(Header(Category));
            userArray.add(SendSMSPage(Category));
        }

        String[] MapData = new String[sCount];
        int idx = 0;
        if (sCount != 0) {
            while (!cursor.isAfterLast()) {
                userArray.add(new User(cursor.getString(cursor.getColumnIndex("Name")), cursor.getString(cursor.getColumnIndex("categoy")), cursor.getString(cursor.getColumnIndex("Intensity")),
                        cursor.getString(cursor.getColumnIndex("Remedies")), cursor.getInt(cursor.getColumnIndex("sublevel")),
                        cursor.getInt(cursor.getColumnIndex("_id")), cursor.getString(cursor.getColumnIndex("selected")), cursor.getString(cursor.getColumnIndex("book")),
                        cursor.getString(cursor.getColumnIndex("newrem")), cursor.getString(cursor.getColumnIndex("json")), cursor.getString(cursor.getColumnIndex("id_web")),
                        cursor.getString(cursor.getColumnIndex("entry")), SLAc.RestoreSessionIndexID("session_id"), cursor.getString(cursor.getColumnIndex("lat_lng"))));

                MapData[idx++] = cursor.getString(cursor.getColumnIndex("Name")) + "#-#" + cursor.getString(cursor.getColumnIndex("lat_lng")) + "#-#" + cursor.getString(cursor.getColumnIndex("newrem"));
                cursor.moveToNext();
            }

            SLAc.SaveArrayPreference("MapDataArray",MapData);
        }
        if ( Category.indexOf("|") != -1) {
            userArray.add(AddNewEntry());
        }

		UserCustomAdapter  userAdapter = new UserCustomAdapter(ctx, R.layout.listview_repertory_details,userArray,Category);

		return userAdapter;


				
	}


}