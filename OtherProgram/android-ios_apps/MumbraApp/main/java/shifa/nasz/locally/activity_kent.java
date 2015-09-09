package shifa.nasz.locally;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.GridView;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.SimpleCursorAdapter;
import android.widget.TextView;

import java.util.ArrayList;

@SuppressLint("NewApi")
public class activity_kent extends Activity  {

    private static String TAG = "MainActivity";
	private SimpleCursorAdapter CursorAdapter;
	public Cursor cursor;
	private ListView myList;
    private GridView gridView;
	ProgressDialog progressDialog ;
	Handler mHandler;
	boolean chatactive = true;
	public String notification_id = "";
	public int notifyid = 1;
	public String SessionID  = "";
	String Category,Category1 = "";
    String HomeCategory = "MUMBRA"; // upper case required in query
    String NameTitle = "";
	String COLSREPERTORY = " _id,Name,categoy,Intensity,Remedies,newrem,sublevel,selected,book,json,id_web,entry,lat_lng ";
    String OrderBY = "  order by json desc ";
	ArrayList<User> userArray = new ArrayList<User>();
	 UserCustomAdapter userAdapter;

	 DBclass KentDB = new DBclass();
	 String LayOut = "Category";
		Super_Library_AppClass SLAc;
	 Context ctx;
	 String SearchType = "";
	String SearchStart = "Mumbra";
	String book = "Kent";
	public DBHelper db1;
	public String QueryChangedForUpdateList = "";
	private boolean ReversedRepertory = false;
	private boolean HomePage;
	private Thread timer;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
	{
		super.onCreate(savedInstanceState);
		ctx = this;
		setContentView(R.layout.activity_kent);
        SLAc = new Super_Library_AppClass(this);
		SessionID  = SLAc.RestoreSessionIndexID("session_id");
		SearchType = RestoreSaveSearchType();
		myList = (ListView)findViewById(R.id.lv_result);
        gridView = (GridView) findViewById(R.id.gridViewHomeMenu);

        db1 = new DBHelper(this);
        KentDB = new DBclass(this);


        if (!SLAc.GetPreferenceValue("LastQry").equals("0")){
            String[] LastQryParam = SLAc.GetPreferenceValue("LastQry").split("#_#");
            if (LastQryParam.length > 2) {
                ExecuteDB(LastQryParam[0], LastQryParam[1], LastQryParam[2]);
            }
            SLAc.SavePreference("LastQry","0");
        }
        else {
            //     populatedatabase("SELECT _id,Name FROM tbl_shifa where level = '0' order by id_web"  , "Home", false);
            CreateQueryExecute(HomeCategory);
        }
	 	Log.e("database","dbclaasdasdasssdn");
	 	int iLastScroll = 0;
	 		 	myList.setOnItemClickListener(new OnItemClickListener() {



		@SuppressLint("DefaultLocale")
		@Override
		public void onItemClick(AdapterView<?> listView, View view, int position,
				long id) {
            String Name = ((TextView) view.findViewById(R.id.txtName_l_r_d)).getText().toString();
            String EntryCounter = ((TextView) view.findViewById(R.id.txtCounter_l_r_d)).getText().toString();
            String    SearchStart =  ((TextView) view.findViewById(R.id.txtName_l_r_d)).getTag().toString();

            ExecuteDB(Name,SearchStart, EntryCounter);

		}
	});

        gridView.setOnItemClickListener(new OnItemClickListener() {
            public void onItemClick(AdapterView<?> parent, View v,
                                    int position, long id) {
                String Name = ((TextView) v.findViewById(R.id.grid_item_label)).getText().toString();
                String EntryCounter = ((TextView) v.findViewById(R.id.grid_item_counter)).getText().toString();
            //    Toast.makeText(getApplicationContext(),
              //          Name, Toast.LENGTH_SHORT).show();
                ExecuteDB(Name,SearchStart, EntryCounter);
            }
        });
	 	Bind();

	}

	}
    private void ExecuteDB(String NameEntry, String CategoryEntry, String EntryCounter){
        try
        {

            String  Name1 = NameEntry;

            if (NameEntry.indexOf("Add New Entry") != -1)
            {
                openNewEntry("");
                return;
            }

            SearchStart = CategoryEntry; //((TextView) view.findViewById(R.id.txtCategory_l_r_d)).getText().toString();
            SLAc.SavePreference("EntryRead"+SearchStart+"|"+NameTitle, EntryCounter);
            StopUser(SearchStart);
            Category1 = Category = SearchStart.replaceAll(", ", "|");
            Category = Category + "|" + NameEntry;
            SearchStart = Category;
            if (Name1.indexOf(", ") != -1)
            {
                Name1 = Name1.substring(0, Name1.indexOf(", "));
            }

            Category1 = Category1 + "|" + Name1;

            Category = Category.toUpperCase();
            Category1 = Category1.toUpperCase();
            Category = Category.replaceAll(", ", "|");
            Category1 = Category1.replaceAll(", ", "|");

            SLAc.SavePreference("LastQry",NameEntry + "#_#" + CategoryEntry + "#_#" + EntryCounter);
            CreateQueryExecute(Category1);
        }
        catch(Exception ex)
        {

            Log.e("Error in listview click", ex.toString());
        }
    }
    private void CreateQueryExecute(String WhereCategory){
        Log.e("Query CreateQueryExecute", WhereCategory);
        WhereCategory = WhereCategory.toUpperCase();

        if (WhereCategory.toUpperCase().indexOf("SELECT") != -1){
            populatedatabase(WhereCategory , "Category", false);

        }
        else if (WhereCategory.indexOf("|") == -1) {
            populatedatabase("SELECT " + COLSREPERTORY + " FROM tbl_shifa where  upper(categoy) = '" + HomeCategory + "'  order by Name" , "HomeMenu", false);
        }
        else{
            populatedatabase("SELECT " + COLSREPERTORY + " FROM tbl_shifa where   upper(categoy) = '" + WhereCategory + "'  " + OrderBY, "Category", false);

        }
    }
   	private void StopUser(String text){
            if (text.equalsIgnoreCase(" Refresh")) {

               /* Intent intent = new Intent(activity_kent.this, activity_settings.class);
                    intent.putExtra("where", "1");
                    intent.putExtra("cols", "id_web,book,newrem,maincategoy,sublevel,Name,remedies,Intensity,categoy,level,selected,entry,lat_lng");
                    startActivity(intent);
             //   finish();*/
                SLAc.FetchData();
            }

	}
	public void Bind()
	{





	}
    public String getTableInfo() {
        StringBuilder b = new StringBuilder("");
        Cursor c = null;
        try {
            String query = "pragma table_info(tbl_shifa)";
            c = db1.getReadableDatabase().rawQuery(query, null);
            if (c.moveToFirst()) {
                do {
                    b.append("Col:" + c.getString(c.getColumnIndex("name")) + " ");
                    b.append(c.getString(c.getColumnIndex("type")));
                    b.append("\n");

                } while (c.moveToNext());
            }
            Log.e(TAG,"tbl details: " + b.toString());
            return b.toString();
        }
        finally {
            if (c != null) {
                c.close();
            }
            if (db1 != null) {
                db1.close();
            }
        }
    }

	@SuppressWarnings("deprecation")
	public void populatedatabase(String sql, String ScreenLayout, boolean back)
	{

        if (sql == null || sql.toUpperCase().indexOf("SELECT") == -1) return;
		Log.e("sql ", sql);
        //Toast.makeText(getApplicationContext(), "Expanding...", 100).show();
        try {
            cursor = db1.getReadableDatabase().rawQuery(sql, null);
        }
        catch(Exception ex){
           Log.e(TAG,"Error in populating query ",ex);
           /* DBHelper db = new DBHelper(this);
            db.initializeDataBase();
            StopUser(" Refresh");*/
        }
		int sCount = cursor.getCount();
		Log.e("SQL Count", String.valueOf(sCount));
		if (sCount == 0 )
		{
               if (sql.indexOf("level = '0'") != -1) {
                   StopUser(" Refresh");
                   return;

               }

			}
		LayOut = ScreenLayout;
		Log.e("LayOut","LayOut " + LayOut);

		/*if (ScreenLayout.equals("Home"))
		{
			String[] ChapterName = new String[cursor.getCount()];
			cursor.moveToFirst();
			String JsonString = "";
			int i = 0;
			while(!cursor.isAfterLast()) {

				ChapterName[i] = cursor.getString(cursor.getColumnIndex("Name"));
				i++;
				cursor.moveToNext();
			}


			UserCustomAdapterHome CursorAdapter = new UserCustomAdapterHome(activity_kent.this, ChapterName);
			HomePage = true;
			myList.setAdapter(CursorAdapter);
		}
		else*/
        if (ScreenLayout.equals("HomeMenu")){
            String[] MOBILE_OS = new String[sCount];
            String[] Record_OS = new String[sCount];
            int i = 0;
            cursor.moveToFirst();
            while (!cursor.isAfterLast()) {

                MOBILE_OS[i] = cursor.getString(cursor.getColumnIndex("Name"));
                Record_OS[i] = cursor.getString(cursor.getColumnIndex("entry"));
                i++;
                cursor.moveToNext();
            }

            gridView.setAdapter(new UserCustomAdapterGrid(this, MOBILE_OS,Record_OS));
            myList.setVisibility(View.GONE);
            gridView.setVisibility(View.VISIBLE);
          //  gridView.setAdapter(adapter);

        }
        else if (ScreenLayout.equals("Category"))
		{
         //   getTableInfo();
         //   Log.e(TAG, "Cursor "+ cursor);
	        userAdapter = KentDB.KentItemListing(this, cursor, userArray, SearchStart,sCount);
			myList.setItemsCanFocus(false);
			myList.setAdapter(userAdapter);

			String[] NameTitle = SearchStart.split("\\|");
            try{
                this.getActionBar().setTitle(NameTitle[NameTitle.length - 1]);
            }
            catch (NullPointerException ex){
               //     this.getActionBar().setTitle("Locally");

            }
            myList.setVisibility(View.VISIBLE);
            gridView.setVisibility(View.GONE);


        }
        if (cursor != null) {
            cursor.close();
        }
        if (db1 != null) {
            db1.close();
        }
	//

	}

	public boolean OldPhone()
	{
		return false;
		/*if (android.os.Build.VERSION.SDK_INT  == 7 || android.os.Build.VERSION.SDK_INT  == 8 || android.os.Build.VERSION.SDK_INT  == 9 || android.os.Build.VERSION.SDK_INT  == 10 || android.os.Build.VERSION.SDK_INT  == 11 || android.os.Build.VERSION.SDK_INT  == 12 || android.os.Build.VERSION.SDK_INT  == 13)
			return true;
		else
			return false;
			*/
	}
	public void KentSearch()
	{



		progressDialog =  ProgressDialog.show(activity_kent.this, "","Loading.. all data for search", true);


    	if (!SearchStart.equals(""))
    	{
    		Thread thread = new Thread() {

    		    @Override
    		    public void run() {

    		        // Block this thread for 2 seconds.
    		        try {
    		            Thread.sleep(2000);
    		        } catch (InterruptedException e) {
    		        }

    		        // After sleep finished blocking, create a Runnable to run on the UI Thread.
    		        runOnUiThread(new Runnable() {
    		            @Override
    		            public void run() {

    		            	SearchStart = SearchStart.replaceAll(", ", "|").toUpperCase();

    		            	String sql =  "";
                            String q = "select "+COLSREPERTORY+" from tbl_shifa   " + sql;
                            populatedatabase(q, "Category",true);

    		            	progressDialog.dismiss();
    		            }
    		        });

    		    }

    		};

    		// Don't forget to start the thread.
    		thread.start();



    	}
    	else
    	{
    		progressDialog.dismiss();
    	}


	}
	@Override
    protected void onStop() {
        super.onStop();
        chatactive = false;
        db1.close();

        Log.e("Chat Activity","user not longer in the application");

    }


    @Override
    protected void onStart() {
        super.onStart();
          //  Toast.makeText(getApplicationContext(), "Service Start." + iHit, 100).show();
          //  if(iHit >= 1){
          //      populatedatabase(sHistory[iHit][0],sHistory[iHit][1],true);
          //  }

        try {
            final String RecordAddedStatus = SLAc.GetPreferenceValue("RecordAddedStatus");
            if (RecordAddedStatus.equalsIgnoreCase("Added Successfully")){
               CreateQueryExecute(SearchStart);
            }
        }catch(Exception ex){

        }
    }
	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {

		if (keyCode == KeyEvent.KEYCODE_BACK) {
	        //code to reset view
			backRepertory();
	        return true;
	    }
	    return super.onKeyDown(keyCode, event);
	}
	private void backRepertory(){
        Log.e(TAG,"SearchStart: "+SearchStart);
        if (SearchStart.indexOf("|") == -1){
            finish();
        }

    	else
    	{

            String[] CatSearchStart = SearchStart.split("\\|");
            String NewCategoryString = SearchStart.substring(0,(SearchStart.length() - (CatSearchStart[CatSearchStart.length - 1].length()+1)));
            SearchStart = NewCategoryString;

            Log.e("SearchStart",SearchStart +  " = " + NewCategoryString);

            CreateQueryExecute(NewCategoryString);
          //  SearchStart = sHistory[iHit][2];
    		//populatedatabase(sHistory[iHit][0],sHistory[iHit][1],true);

    	}
	}
	@Override
	  public void onDestroy() {
	    super.onDestroy();
	    if (cursor != null) {
            cursor.close();
        }
        if (db1 != null) {
            db1.close();
        }
	  }

	private void SaveSearchType(String sTypeName)
	{
  	  SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
  	  editor.putString("SaveSearchType", sTypeName);
  	  editor.commit();
  	  SearchType = sTypeName;

	}
	private void SaveShifaConnectType(String sTypeName)
	{
  	  SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
  	  editor.putString("SaveShifaConnectType", sTypeName);
  	  editor.commit();
  	  SearchType = sTypeName;

	}
	private String RestoreSaveSearchType()
	{
		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0);
		String restoredText = prefs.getString("SaveSearchType", null);
		if (restoredText != null)
		{
			return restoredText;
		}
		return "";

	}
	private String RestoreShifaConnectType()
	{
		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0);
		String restoredText = prefs.getString("SaveShifaConnectType", null);

		if (restoredText != null)
		{
			return restoredText;
		}
		if (restoredText == null){
			restoredText = "1";
		}
		return restoredText;

	}


	@SuppressLint("NewApi")
	@Override
	  public boolean onCreateOptionsMenu(Menu menu) {
        super.onCreateOptionsMenu(menu);
        if (OldPhone() == true )
		{
			  MenuInflater mi = getMenuInflater();
			  mi.inflate(R.menu.kent, menu);
			  return true;
		}
		else
		{
			getMenuInflater().inflate(R.menu.kent, menu);
		    SearchView searchView = (SearchView) menu.findItem(R.id.menu_refresh).getActionView();

		    searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {

		        @Override
		        public boolean onQueryTextChange(String newText) {
                    try
                    {

                        userAdapter.getFilter().filter(newText);
                        myList.setAdapter(userAdapter);
                        if (myList.getCount() == 0){
                            DaemonThreadStart(newText);
                        }
                    }
                    catch(Exception ex){
                        Log.e(TAG,"Error Error Action bar search category",ex);
                    }
		            return false;
		        }


		        @Override
		        public boolean onQueryTextSubmit(String query) {

		            return false;
		        }
		    });



		}

		 return super.onCreateOptionsMenu(menu);
	  }
    private void SearchInDatabase(String SearchTerm){
        String query = SearchTerm;
        if (query.length() != 0) {
            try
            {
                final String SearchStart = query.toUpperCase();

                Thread thread = new Thread() {
                    @Override
                    public void run() {

                        // Block this thread for 2 seconds.
                        try {
                            Thread.sleep(1000);

                        } catch (InterruptedException e) {

                        }

                        // After sleep finished blocking, create a Runnable to run on the UI Thread.
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                String whereClause = "";


                                if (SearchStart.indexOf(" ")!= -1){
                                    String[] GetCatName = SearchStart.split(" ");
                                    for(int i =0;i<=GetCatName.length - 1; i++){
                                        if (!whereClause.equals("")) whereClause += " or ";
                                        whereClause += " upper(categoy) like '%" + GetCatName[i] + "%' or ";

                                        whereClause += " upper(name) like '%" + GetCatName[i] + "%' or ";
                                        whereClause += " upper(remedies) like '%" + GetCatName[i] + "%' ";
                                    }
                                }
                                else
                                {
                                    whereClause += " upper(categoy) like '%" + SearchStart + "%' or ";
                                    whereClause += " upper(name) like '%" + SearchStart + "%' or ";
                                    whereClause += " upper(remedies) like '%" + SearchStart + "%' ";

                                }

                                String sql =  "";
                                sql =  " where "+ whereClause + " " + OrderBY;
                                String q = "select "+COLSREPERTORY+" from tbl_shifa   " + sql;
                                populatedatabase(q, "Category",true);


                                //progressDialog.dismiss();
                            }
                        });

                    }

                };

                // Don't forget to start the thread.
                thread.start();
            }
            catch(Exception ex){
                Log.e(TAG,"Error app Action bar search",ex);}
            // handle search here
        }
    }
    private DaemonThread threadQuerySearch = null;

    class DaemonThread extends Thread {
        private String SearchActionBar = "";
        public DaemonThread(String SearchTerm){
            SearchActionBar = SearchTerm;
        }
        public void run()
        {
            try{
                Thread.sleep(500);
                Log.e(TAG,"Searching using daemonthread ..");
                SearchInDatabase(SearchActionBar);
            }
            catch(InterruptedException ex){

            }
            catch(Exception ex){
                Log.e(TAG, "DaemonThread -> Run",ex);
            }
        }
    }
    private void DaemonThreadStart(String SearchActionBar){
        if (threadQuerySearch != null){
            threadQuerySearch.interrupt();

            threadQuerySearch = null;
        }
        threadQuerySearch = new DaemonThread(SearchActionBar);
        threadQuerySearch.start();

    }

	 public boolean onPrepareOptionsMenu(Menu menu) {



		    return true;
		}
	public boolean onOptionsItemSelected(MenuItem item) {
		switch(item.getItemId())
        {
            case R.id.menu_refresh:

             //   CommonSearch();
            	return true;
            case R.id.menu_update:
                StopUser(" Refresh");

                //CommonSearch();
                return true;
            case R.id.menu_addNew:
            	//ShifaFeed();
            	//final MenuItem  img_internet_Connected = (MenuItem)findViewById(R.id.menu_feed);
             //   Toast.makeText(getApplicationContext(), "iHit ." + iHit, 100).show();
                if (SearchStart.indexOf("|") == -1) {
                    openNewEntry("Category");
                }
                else
                {
                    openNewEntry("");
                }
                return true;
            case R.id.menu_back:
                //ShifaFeed();
                //final MenuItem  img_internet_Connected = (MenuItem)findViewById(R.id.menu_feed);
                backRepertory();
                return true;
            case R.id.menu_flush_cache:
                //ShifaFeed();
                //final MenuItem  img_internet_Connected = (MenuItem)findViewById(R.id.menu_feed);
                SLAc.ClearHistory();
                return true;

            default:
                return super.onOptionsItemSelected(item);

        }
	}

    private void openNewEntry(String PageName){
        //Toast.makeText(getApplicationContext(), "SearchStart ." + SearchStart, 100).show();
        SLAc.SavePreference("CategoryID",SearchStart);


        if (SearchStart.indexOf("|") == -1){
            Intent intent = new Intent(activity_kent.this, activity_addnew_category.class);
            startActivity(intent);
        }
        else /*if (HomePage == true) {
            Intent intent = new Intent(activity_kent.this, activity_addnew_area.class);
            startActivity(intent);
        }
        else if (HomePage == false) {*/
        {
            Intent intent = new Intent(activity_kent.this, activity_addnew.class);
            startActivity(intent);
        }

    }
	  
	   
	   
}
