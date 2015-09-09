package shifa.nasz.locally;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

public class DBHelper extends SQLiteOpenHelper {
	Context context;
    String TAG = "DBHelper";
	public String DB_PATH;
	public InputStream DB_PATH_IN;
	private static final String DATABASE_NAME = "testdb.db";
	private static final int DATABASE_VERSION = 8;
    private SQLiteDatabase db;
	public DBHelper(Context context) {
		super(context, DATABASE_NAME, null, DATABASE_VERSION);
    //    db = getWritableDatabase();
     //   Log.e(TAG,"DB Established");
        try {
         //   DB_PATH_IN = (InputStream) context.getAssets().open(DATABASE_NAME);
            DB_PATH = context.getDatabasePath(DATABASE_NAME).getAbsolutePath();

            Log.e(TAG,"DBHelper " + DB_PATH);
           /* try {
                copyStream(DB_PATH_IN, new FileOutputStream(DB_PATH));
            } catch (IOException e1) {
                Log.e(TAG,"Error  e1" , e1);
                e1.printStackTrace();
            }*/
            //Log.e(TAG,"DB_PATH_IN " + "Copied");

        } catch (Exception e) {
            //Log.e(TAG,"Error ",e);

        }
//        db = getWritableDatabase();
        //Cursor cursor = db.rawQuery("Select * from tbl_shifa",null);
        //Log.e(TAG,"Count: " + cursor.getCount());
	}
   /* @Override
    public synchronized SQLiteDatabase getWritableDatabase() {
        try {
            if (db != null) {
                if (db.isOpen()) {
                    return db;
                }
            }
            return SQLiteDatabase.openDatabase(DB_PATH, null, SQLiteDatabase.OPEN_READWRITE | SQLiteDatabase.NO_LOCALIZED_COLLATORS);
        }
        catch (Exception e) {
            Log.e(TAG,"Exception ",e);
            return null;
        }
    }*/
    @Override
    public synchronized void close() {
        if (db != null) {
            db.close();
            db = null;
        }
        super.close();
    }
	public void copyStream(InputStream is, OutputStream os) throws IOException {

		byte buf[] = new byte[1024];
		int c = 0;
		while (true) {
			c = is.read(buf);
			if (c == -1)
				break;
			os.write(buf, 0, c);
		}
		is.close();
		os.close();
	}



	public void initializeDataBase() {

	    	getWritableDatabase();
		    Log.e("DBHelper initializeDataBase ", "coping database");
			Log.e("DBHelper createDatabase ", "true");
			try {
				copyStream(DB_PATH_IN, new FileOutputStream(DB_PATH));
			}
			catch (Exception e) 
			{
				throw new Error("Error copying database");
			}

		    Log.e("DBHelper clear  ", "true");

	}

	@Override
	public void onCreate(SQLiteDatabase db) {

		Log.e("DBHelper", "onCreate");

		// db.execSQL(USER_MASTER_CREATE);

	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		// if DATABASE VERSION changes
		// Drop old tables and call super.onCreate()
		//upgradeDatabase = true;
	}

}
