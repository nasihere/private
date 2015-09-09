package com.diechart.database;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class DatabaseHandler extends SQLiteOpenHelper {

	private static int DB_VERSION = 3;
	private static String DATABASE_NAME = "die_db";
	private static String DATABASE_TABLE = "die_summary";
	private static String ID = "id";
	private static String CODE = "code";
	private static String DESC = "desc";
	private static String ID1 = "id1";
	private static String ID2 = "id2";
	private static String LENGTH = "length";
	private static String PRICE_VAL = "price_val";
	private static String GLASS_SIZE = "glass_size";
	private static String ID1_ID2 = "id1_id2";
	private Context context;
	private Cursor cursor;

	public DatabaseHandler(Context context) {
		super(context, DATABASE_NAME, null, DB_VERSION);
		// TODO Auto-generated constructor stub
		this.context = context;
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		// TODO Auto-generated method stub
		String table1 = "CREATE TABLE " + DATABASE_TABLE + "(" + ID
				+ " INTEGER PRIMARY KEY ," + CODE + " TEXT, " + DESC + " TEXT,"
				+ ID1 + " TEXT," + ID2 + " TEXT," + LENGTH + " TEXT,"
				+ PRICE_VAL + " TEXT," + GLASS_SIZE + " TEXT," + ID1_ID2
				+ " TEXT )";
		try {
			db.execSQL(table1);
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	@Override
	public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
		// TODO Auto-generated method stub
		try {
			db.execSQL("DROP TABLE IF EXISTS " + DATABASE_TABLE);

			// Create tables again
			onCreate(db);
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public Boolean insertRecords(String result) {

		Boolean status = false;
		SQLiteDatabase db = null;
		// DatabaseHandler db = new DatabaseHandler(context);
		try {
			JSONArray jArray = new JSONArray(result);
			for (int i = 0; i < jArray.length(); i++) {
				db = this.getWritableDatabase();
				JSONObject jObject = jArray.getJSONObject(i);
				ContentValues values = new ContentValues();
				values.put(ID, jObject.getString("id"));
				values.put(CODE, jObject.getString("Code"));
				values.put(DESC, jObject.getString("Description"));
				values.put(ID1, jObject.getString("Id1"));
				values.put(ID2, jObject.getString("Id2"));
				values.put(LENGTH, jObject.getString("Length"));
				values.put(PRICE_VAL, jObject.getString("Price_val"));
				values.put(GLASS_SIZE, jObject.getString("Glass_Size"));
				values.put(ID1_ID2, jObject.getString("ID1_ID2"));
				// Inserting Row
				db.insert(DATABASE_TABLE, null, values);

			}
			status = true;
			db.close(); // Closing database connection

		} catch (JSONException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			status = false;
		}

		return status;
	}

	public String fetchAllrow() {

		// String selectQuery = "SELECT  * FROM " + DATABASE_TABLE;
		SQLiteDatabase db = this.getWritableDatabase();
		// Cursor cursor = db.rawQuery(selectQuery, null);
		JSONArray jArray = new JSONArray();
		try {
			cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE, null);
			// sJSONArray jArray = new JSONArray();
			if (cursor.moveToFirst()) {
				do {
					JSONObject jObject = new JSONObject();
					String[] clName = cursor.getColumnNames();
					for (int i = 0; i < clName.length; i++) {
						try {
							jObject.put(clName[i], cursor.getString(i));
						} catch (JSONException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
					}
					// jObject.put("Id", cursor.getColumnNames("id"));
					jArray.put(jObject);

				} while (cursor.moveToNext());
			}
		} catch (Exception e) {
			// TODO: handle exception
			e.printStackTrace();
		}
		return jArray.toString();
	}
}
