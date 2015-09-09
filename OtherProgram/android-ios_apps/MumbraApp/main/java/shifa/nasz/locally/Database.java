package shifa.nasz.locally;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.os.Environment;

/**
 * Created by Nasz on 2/8/15.
 */
public class Database extends SQLiteOpenHelper {

    public static final String DATABASE_NAME = "testdb";
    private String DATABASE_PATH = null;
    private static final int DATABASE_VERSION = 1;
    private static final String PACKAGE_NAME = "gcmtutorial.androidbegin.com.databasetest";
    private SQLiteDatabase db;

    public Database(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        DATABASE_PATH = getDatabaseFolder()+DATABASE_NAME;
        db = getWritableDatabase();
    }

    public static String getDatabaseFolder() {
        return Environment.getExternalStorageDirectory().getAbsolutePath()+"/Android/data/"+PACKAGE_NAME+"/databases/";
    }

    @Override
    public synchronized SQLiteDatabase getWritableDatabase() {
        try {
            if (db != null) {
                if (db.isOpen()) {
                    return db;
                }
            }
            return SQLiteDatabase.openDatabase(DATABASE_PATH, null, SQLiteDatabase.OPEN_READWRITE | SQLiteDatabase.NO_LOCALIZED_COLLATORS);
        }
        catch (Exception e) {
            return null;
        }
    }

    @Override
    public synchronized void close() {
        if (db != null) {
            db.close();
            db = null;
        }
        super.close();
    }

    @Override
    public void onCreate(SQLiteDatabase db) { }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) { }

}
