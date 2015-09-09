package shifa.nasz.locally;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import org.apache.http.util.ByteArrayBuffer;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URL;
import java.net.URLConnection;


public class DBSQLHelper extends SQLiteOpenHelper {

    //The Android's default system path of your application database.
    private String DB_PATH = "/data/data/YOUR_PACKAGE/databases/";

    private static String DB_NAME = "testdb";
    private static String TAG = "DBSQLHelper";
    private SQLiteDatabase myDataBase;

    private final Context ctx;

    /**
     * Constructor
     * Takes and keeps a reference of the passed context in order to access to the application assets and resources.
     * @param context
     */
    public DBSQLHelper(Context context) {

        super(context, DB_NAME, null, 1);
        Log.e(TAG,"DBSQLHelper Helper begin");
        DB_PATH = context.getDatabasePath(DB_NAME).getAbsolutePath();
        this.ctx = context;
        try {
            Log.e(TAG,"DBSQLHelper Create database or check db begin");
            createDataBase();
        } catch (IOException e) {
            Log.e(TAG,"Error DBSQLHELPER",e);
            e.printStackTrace();
        }
        Log.e(TAG,"DBSQLHelper Helper finished loading " + DB_PATH);
    }

    /* Download DB */
    public void DownloadDatabase(){
        (new Thread(new Runnable() {

            @Override
            public void run() {
                try {
                    Log.e(TAG,"Downloading..");
                    Thread.sleep(10);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                try {

                    Log.e(TAG,"Downloading.. Start");
                    // Log.d(TAG, "downloading database");
                    URL url = new URL("http://kent.nasz.us/mumbra/database/testdb");
                    URLConnection ucon = url.openConnection();
                    InputStream is = ucon.getInputStream();
                    BufferedInputStream bis = new BufferedInputStream(is);

                    ByteArrayBuffer baf = new ByteArrayBuffer(1024);
                    int current = 0;
                    while ((current = bis.read()) != -1) {
                        baf.append((byte) current);
                    }

                    Log.e(TAG,"Downloading.. Writing");
                    /* Convert the Bytes read to a String. */



                    OutputStream myOutput = new FileOutputStream(DB_PATH + "_server");
                    myOutput.write(baf.toByteArray());
                    myOutput.flush();
                    myOutput.close();
                    bis.close();
                    is.close();
                    Log.e(TAG,"Downloading.. Writing finish " + DB_PATH + "_server");
                    copyDataBase();

                } catch (Exception exe) {

                    Log.e(TAG,"Downloading.. Error ",exe);

                }

            }
        })).start();

    }
    /**
     * Creates a empty database on the system and rewrites it with your own database.
     * */
    public void createDataBase() throws IOException {


        boolean dbExist = checkDataBase();

        if(dbExist){
            Log.e(TAG,"Database Already exits");
            //do nothing - database already exist
        }else{

            //By calling this method and empty database will be created into the default system path
            //of your application so we are gonna be able to overwrite that database with our database.
            this.getReadableDatabase();

            try {

                //copyDataBase();
                DownloadDatabase();

            } catch (Exception e) {

                throw new Error("Error copying database");

            }
        }

    }

    /**
     * Check if the database already exist to avoid re-copying the file each time you open the application.
     * @return true if it exists, false if it doesn't
     */
    private boolean checkDataBase(){

        File file = new File(DB_PATH);
        return file.exists();
        /*SQLiteDatabase checkDB = null;

        try{
            String myPath = DB_PATH;
            Log.e(TAG,"DBPath checking is exists " + myPath);
            checkDB = SQLiteDatabase.openDatabase(myPath, null, SQLiteDatabase.OPEN_READONLY);

        }catch(SQLiteException e){

            //database does't exist yet.

            Log.e(TAG,"CreateDatabase Database Doesn't exits yet.. PLs Download it on server");

        }

        if(checkDB != null){

            checkDB.close();

        }

        return checkDB != null ? true : false;*/
    }

    /**
     * Copies your database from your local assets-folder to the just created empty database in the
     * system folder, from where it can be accessed and handled.
     * This is done by transfering bytestream.
     * */
    private void copyDataBase() throws IOException{

        //Open your local db as the input stream
//Open your local db as the input stream
        InputStream myInput = new FileInputStream(DB_PATH + "_server");

        // Path to the just created empty db
        String outFileName = DB_PATH;

        //Open the empty db as the output stream
        OutputStream myOutput = new FileOutputStream(outFileName);

        //transfer bytes from the inputfile to the outputfile
        byte[] buffer = new byte[1024];
        int length;
        while ((length = myInput.read(buffer))>0){
            myOutput.write(buffer, 0, length);
        }

        //Close the streams
        myOutput.flush();
        myOutput.close();
        myInput.close();
        Log.e(TAG,"Copied Database " + outFileName);
    }

    public void openDataBase() throws java.sql.SQLException{

        //Open the database
        String myPath = DB_PATH + DB_NAME;
        Log.e(TAG,"Database Path "+ myPath);
        myDataBase = SQLiteDatabase.openDatabase(myPath, null, SQLiteDatabase.OPEN_READONLY);

    }

    @Override
    public synchronized void close() {

        if(myDataBase != null)
            myDataBase.close();

        super.close();

    }

    @Override
    public void onCreate(SQLiteDatabase db) {

    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }

    // Add your public helper methods to access and get content from the database.
    // You could return cursors by doing "return myDataBase.query(....)" so it'd be easy
    // to you to create adapters for your views.

}