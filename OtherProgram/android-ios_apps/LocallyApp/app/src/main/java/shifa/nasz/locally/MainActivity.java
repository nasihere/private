package shifa.nasz.locally;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.WindowManager;
import android.widget.Toast;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;


public class MainActivity extends Activity {

    private static final String SD_CARD_FOLDER = "MyApp";
    String TAG = "MainActivity";
    private static final String DB_DOWNLOAD_PATH = "http://kent.nasz.us/mumbra/database/testdb.db";
    private DBHelper mDB = null;
    private DatabaseDownloadTask mDatabaseDownloadTask = null;
    private DatabaseOpenTask mDatabaseOpenTask = null;
    String DATABASE_NAME = "testdb.db";
    String DB_PATH = "";
    String DB_Root = "";
    public ProgressDialog mProgressDialog;
    private class DatabaseDownloadTask extends AsyncTask<Context, Integer, Boolean> {

        @Override
        protected void onPreExecute() {
            mProgressDialog = new ProgressDialog(MainActivity.this);
            mProgressDialog.setTitle(getString(R.string.please_wait));
            mProgressDialog.setMessage(getString(R.string.downloading_database));
            mProgressDialog.setIndeterminate(false);
            mProgressDialog.setMax(100);
            mProgressDialog.setProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
            mProgressDialog.setCancelable(false);
            mProgressDialog.show();
            getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        }

        @Override
        protected Boolean doInBackground(Context... params) {
            try {
                File dbDownloadPath = new File(DB_Root);
                Log.e(TAG,"Download Path: " +dbDownloadPath.getPath());
              //  File dbDownloadPath = new File(cacheDir);
                if (!dbDownloadPath.exists()) {
                    Log.e(TAG,"Folder creating database");
                    dbDownloadPath.mkdirs();
                }
                HttpParams httpParameters = new BasicHttpParams();
                HttpConnectionParams.setConnectionTimeout(httpParameters, 5000);
                HttpConnectionParams.setSoTimeout(httpParameters, 5000);
                DefaultHttpClient client = new DefaultHttpClient(httpParameters);
                HttpGet httpGet = new HttpGet(DB_DOWNLOAD_PATH);
                InputStream content = null;
                try {
                    HttpResponse execute = client.execute(httpGet);
                    if (execute.getStatusLine().getStatusCode() != 200) { return null; }
                    content = execute.getEntity().getContent();
                    long downloadSize = execute.getEntity().getContentLength();
                    FileOutputStream fos = new FileOutputStream(DB_PATH);
                    byte[] buffer = new byte[256];
                    int read;
                    long downloadedAlready = 0;
                    while ((read = content.read(buffer)) != -1) {
                        fos.write(buffer, 0, read);
                        downloadedAlready += read;
                        publishProgress((int) (downloadedAlready*100/downloadSize));
                    }
                    fos.flush();
                    fos.close();
                    content.close();
                    Log.e(TAG,"File Created");
                    return true;
                }
                catch (Exception e) {
                    if (content != null) {
                        try {
                            content.close();
                        }
                        catch (IOException e1) {}
                    }
                    Log.e(TAG,"Error " + e.toString());
                    return false;
                }
            }
            catch (Exception e) {
                return false;
            }
        }

        protected void onProgressUpdate(Integer... values) {
            if (mProgressDialog != null) {
                if (mProgressDialog.isShowing()) {
                    mProgressDialog.setProgress(values[0]);
                }
            }
        }

        @Override
        protected void onPostExecute(Boolean result) {
            if (mProgressDialog != null) {
                mProgressDialog.dismiss();
                mProgressDialog = null;
            }
            if (result.equals(Boolean.TRUE)) {
                Toast.makeText(MainActivity.this, getString(R.string.database_download_success), Toast.LENGTH_LONG).show();
                mDatabaseOpenTask = new DatabaseOpenTask();
                Log.e(TAG,"Download Finished");
                mDatabaseOpenTask.execute(new Context[] { MainActivity.this });
            }
            else {
                Toast.makeText(getApplicationContext(), getString(R.string.database_download_fail), Toast.LENGTH_LONG).show();
                finish();
            }
        }

    }

    private class DatabaseOpenTask extends AsyncTask<Context, Void, DBHelper> {

        @Override
        protected DBHelper doInBackground(Context ... ctx) {
            try {
               /* String externalBaseDir = Environment.getExternalStorageDirectory().getAbsolutePath();
                // DELETE OLD DATABASE ANFANG
                File oldFolder = new File(Environment.getExternalStorageDirectory().getAbsolutePath()+"/"+SD_CARD_FOLDER);
                File oldFile = new File(oldFolder, "dictionary.sqlite");
                if (oldFile.exists()) {
                    oldFile.delete();
                }
                if (oldFolder.exists()) {
                    oldFolder.delete();
                }
                */
                // DELETE OLD DATABASE ENDE
                File sdDir = android.os.Environment.getExternalStorageDirectory();
                File dbDownloadPath = new File(DB_PATH);
                File newDB = new File(DB_PATH);
                if (newDB.exists()) {
                    Log.e(TAG, "DB Exists");

                    return new DBHelper(ctx[0]);
                }
                else {
                    return null;
                }
            }
            catch (Exception e) {
                Log.e(TAG,"Error ",e);
                return null;
            }
        }

        @Override
        protected void onPreExecute() {
            mProgressDialog = ProgressDialog.show(MainActivity.this, getString(R.string.please_wait), "Loading the database! This may take some time ...", true);
        }

        @Override
        protected void onPostExecute(DBHelper newDB) {
            if (mProgressDialog != null) {

                mProgressDialog.dismiss();
                mProgressDialog = null;
            }
            if (newDB == null) {
                Log.e(TAG,"DB DOWNLOADED is null");

                mDB = null;
                mDatabaseDownloadTask = new DatabaseDownloadTask();
                mDatabaseDownloadTask.execute();
                /*
                AlertDialog.Builder downloadDatabase = new AlertDialog.Builder(MainActivity.this);
                downloadDatabase.setTitle(getString(R.string.downloadDatabase));
                downloadDatabase.setCancelable(false);
                downloadDatabase.setMessage(getString(R.string.wantToDownloadDatabaseNow));
                downloadDatabase.setPositiveButton(getString(R.string.download), new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();
                        mDatabaseDownloadTask = new DatabaseDownloadTask();
                        mDatabaseDownloadTask.execute();
                    }
                });
                downloadDatabase.setNegativeButton(getString(R.string.cancel), new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();
                        finish();
                    }
                });
                downloadDatabase.show();
                */
            }
            else {
                Log.e(TAG,"DB DOWNLOADED & REGISTERED");
                //mDB = newDB;
                Intent intent = new Intent(MainActivity.this, login.class);
                startActivity(intent);

                finish();
            }
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        if (mDatabaseDownloadTask != null) {
            if (mDatabaseDownloadTask.getStatus() != AsyncTask.Status.FINISHED) {
                mDatabaseDownloadTask.cancel(true);
            }
        }
        if (mDatabaseOpenTask != null) {
            if (mDatabaseOpenTask.getStatus() != AsyncTask.Status.FINISHED) {
                mDatabaseOpenTask.cancel(true);
            }
        }
        if (mProgressDialog != null) {
            mProgressDialog.dismiss();
            mProgressDialog = null;
        }
        if (mDB != null) {
            mDB.close();
            mDB = null;
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.downloaddatabase);
        DB_PATH = this.getDatabasePath(DATABASE_NAME).getAbsolutePath();
        DB_Root = DB_PATH.replace("/"+DATABASE_NAME,"");
        Log.e(TAG,"DB_Root: " + DB_Root);
        File sdDir = android.os.Environment.getExternalStorageDirectory();
        File dbDownloadPath = new File(sdDir,"data/database/");
        File newDB = new File(DB_PATH);
        Log.e(TAG,"newDB: "+newDB.getPath());
        if (newDB.exists()) {
            Log.e(TAG,"DB Already Exists");
            Intent intent = new Intent(MainActivity.this, login.class);
            startActivity(intent);
            finish();
            return;
        }

        if (!Environment.getExternalStorageState().equals(Environment.MEDIA_MOUNTED)) {
            Toast.makeText(getApplicationContext(), getString(R.string.sd_card_not_found), Toast.LENGTH_LONG).show();
            finish();
        }
        mDatabaseOpenTask = new DatabaseOpenTask();
        mDatabaseOpenTask.execute(new Context[] { this });
    }
}
