package com.shifa.employee.logger;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.graphics.Typeface;
import android.hardware.Camera;
import android.hardware.Camera.PictureCallback;
import android.hardware.Camera.ShutterCallback;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.text.Editable;
import android.text.TextWatcher;
import android.text.format.Time;
import android.util.Log;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import net.sourceforge.zbar.Config;
import net.sourceforge.zbar.Image;
import net.sourceforge.zbar.ImageScanner;
import net.sourceforge.zbar.Symbol;
import net.sourceforge.zbar.SymbolSet;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;


@SuppressLint("NewApi")
public class home_menu extends Activity  implements SurfaceHolder.Callback{


    // Camera Pic
	Camera camera;
	SurfaceView surfaceView;
	SurfaceHolder surfaceHolder;
	boolean previewing = false;
	LayoutInflater controlInflater = null;

    /// Camera Picture //
	private static final String TAG = "CameraDemo";
	String OpenAfterPasswordSuccessfull = "";
	String SessionID,CurrentDateTime,CurrentCode = "";
	String session_company_name = "";
	String session_admin_name = "";
	ImageView imgViewCaptureImage;
	String EmployeeId = "";
    LinearLayout linKeyPad;
    LinearLayout linCameraScanner;
	ArrayList<Model_MemberInfo> MemInfo = new ArrayList<Model_MemberInfo>();
	private EditText tvCodeEntered;
	private TextView tvHeaderTitle;
	private Button button0,button1,button2,button3,button4,button5,button6,button7,button8,button9,buttonIn,buttonReset,buttonScanner;
	String Session_data = "";
	Context ctx;
	
	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		ctx = this;
		

            // for fullscreen
		requestWindowFeature(Window.FEATURE_NO_TITLE);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
        WindowManager.LayoutParams.FLAG_FULLSCREEN);
		setContentView(R.layout.home_menu);
		//Get Session id to pass day changed url 
		 SessionID = LoggedIn();
		//after recevied session id we can get new data
		 DayChanged();

        tvHeaderTitle = (TextView) findViewById(R.id.tvTimeFace);
		 imgViewCaptureImage = (ImageView) findViewById(R.id.imgViewCaptureImage);
    	 tvCodeEntered = (EditText) findViewById(R.id.tvCodeEntered);
		 OnBind();
        OnBindKeyPad();
		 MemberInfo();
		 //SavePreference("SyncData",""); // not important 
			 //AutoStartUp();*/
		// UserThemeApply();
		 ResetCompanyMsg(tvHeaderTitle );
		 CameraOnCreate();
		 SyncPicture();
        TimerAction();
        onBindQRCode();

		 
	}
    private void OnBindKeyPad(){
        linKeyPad = (LinearLayout) findViewById(R.id.linKeyPad);

        //Scanner and qrcode bind
        linCameraScanner = (LinearLayout) findViewById(R.id.linCameraScanner);
    }
    private void KeyPadOn(){
       // linKeyPad.setVisibility(View.GONE);
       // linCameraScanner.setVisibility(View.VISIBLE);

    }
    private void KeyPadOff(){
       // linKeyPad.setVisibility(View.VISIBLE);
       // linCameraScanner.setVisibility(View.GONE);

    }
    private void TimerAction(){

        final Handler handler = new Handler();
        Runnable runnable = new Runnable() {

            public void run() {
                while(true) {
                    try {
                        Thread.sleep(5000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    handler.post(new Runnable() {
                        public void run() {
                            SwtichAppNameWithTime();
                        }
                    });
                }
            }
        };

        new Thread(runnable).start();
    }
    int toggleid = 0;

    private void SwtichAppNameWithTime(){

        if (toggleid == 0) {
            Time dtNow = new Time();
            dtNow.setToNow();
            int hours = dtNow.hour;
            String lsNow = dtNow.format("%Y.%m.%d %H:%M");
            String lsYMD = dtNow.toString();    // YYYYMMDDTHHMMSS

            tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.fade_in));
            tvHeaderTitle.setText(lsNow);
            toggleid++;
        }else if (toggleid == 1){
            tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.fade_out));
            if (barcodeScanned) {
                tvHeaderTitle.setText("Card Scanning");
            }
            else{
                tvHeaderTitle.setText("Enter Your PIN");
            }
            toggleid++;
        }
        else if (toggleid == 2){
            tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.fade_in));
            tvHeaderTitle.setText(session_company_name);
            toggleid++;
        }
        else{
            tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.fade_out));
            tvHeaderTitle.setText(getResources().getString(R.string.app_name));
            toggleid = 0;
        }
    }
    private boolean checkNewLogin(){
        Bundle extras = getIntent().getExtras();
        String SessionID = "";
        if (extras != null) {
            String tmp = extras.getString("justlogin");
            if(tmp.equals("true")) {
                Toast.makeText(getApplicationContext(),
                        "Try 123 keycode for demo testing.",
                        Toast.LENGTH_LONG).show();

                return true;
            }
        }
        return false;
    }
	private void CameraOnCreate()
	{
	    //	setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		//getWindow().setFormat(PixelFormat.UNKNOWN);
        //surfaceView = (SurfaceView)findViewById(R.id.camerapreview);
        //surfaceHolder = surfaceView.getHolder();
        //surfaceHolder.addCallback(this);
        //surfaceHolder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
        imgViewCaptureImage.setVisibility(View.GONE);
	}

    ShutterCallback myShutterCallback = new ShutterCallback(){

		@Override
		public void onShutter() {
			// TODO Auto-generated method stub
			
		}};
		
	PictureCallback myPictureCallback_RAW = new PictureCallback(){

		@Override
		public void onPictureTaken(byte[] arg0, Camera arg1) {
			// TODO Auto-generated method stub
			
		}};
	private void CameraOn()
	{
		try
		{

            imgViewCaptureImage.setVisibility(View.GONE);
            previewMain.setVisibility(View.VISIBLE);
			//camera.startPreview();
		//	previewing = true;
			
			
			
		}
		catch(Exception ex)
		{
			Log.e("Camera On",ex.toString());
		}
	}
	private void TakePicture()
	{
		try
		{
			mCamera.takePicture(myShutterCallback,
				myPictureCallback_RAW, myPictureCallback_JPG);
			
		
		}
		catch(Exception ex){}
	}
	PictureCallback myPictureCallback_JPG = new PictureCallback(){

		@Override
		public void onPictureTaken(byte[] arg0, Camera arg1) {
			// TODO Auto-generated method stub
			try
			{
			Matrix matrix = new Matrix();
			matrix.postRotate(-90);

			Bitmap bitmapPicture	= BitmapFactory.decodeByteArray(arg0, 0, arg0.length);
			
			Bitmap rotated = Bitmap.createBitmap(bitmapPicture, 0, 0, bitmapPicture.getWidth(), bitmapPicture.getHeight(),
			        matrix, true);
			imgViewCaptureImage.setImageBitmap(rotated);
			SaveBitMap(CurrentDateTime+"N-A-N"+CurrentCode+"N-A-N"+SessionID,rotated);
			
			
			imgViewCaptureImage.setVisibility(View.VISIBLE);
			mCamera.stopPreview();
			previewing = false;
            CameraScannerOn();
			}
			catch(Exception ex){


            }
		}};
		public  void SaveBitMap(String path,Bitmap _bitmapScaled) {//you can provide file path here 
			try {
				Log.e("SaveBitmap","true");
				File newD = new File(Environment.getExternalStorageDirectory()
                    + File.separator +"shifa_elogger"+ File.separator + "img");
				Log.e("SaveBitmap","true1");
				if(!newD.exists()){
					Log.e("SaveBitmap","true3");
					newD.mkdirs();
					Log.e("SaveBitmap","true4");
				}
				Log.e("SaveBitmap","true2");
				ByteArrayOutputStream bytes = new ByteArrayOutputStream();
			    _bitmapScaled.compress(Bitmap.CompressFormat.JPEG, 40, bytes);
			    Log.e("SaveBitmap","true5");
				//you can create a new file name "test.jpg" in sdcard folder.
				File f = new File( newD + File.separator + path + ".jpg");
					f.createNewFile();
					Log.e("SaveBitmap","true7");
				//write the bytes in file
				FileOutputStream fo = new FileOutputStream(f);
				fo.write(bytes.toByteArray());
				Log.e("SaveBitmap","true9");
				// remember close de FileOutput
				fo.close();
				Log.e("SaveBitmap","true10");
			} catch (IOException e) {
				Log.e("Error",e.toString());
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
	    }
		@SuppressLint("NewApi")
		@Override
		public void surfaceChanged(SurfaceHolder holder, int format, int width,
				int height) {
			// TODO Auto-generated method stub
			if(previewing){
				//camera.stopPreview();
				//previewing = false;
			}
			
			if (camera != null){
				try {
					// make any resize, rotate or reformatting changes here
				    if (this.getResources().getConfiguration().orientation != Configuration.ORIENTATION_LANDSCAPE) {

				        camera.setDisplayOrientation(90);

				    } else {

				        camera.setDisplayOrientation(0);

				    }
					camera.setPreviewDisplay(surfaceHolder);
					//camera.startPreview();
					//previewing = true;
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
	
		 }

		@Override
		public void surfaceCreated(SurfaceHolder holder) {
			// TODO Auto-generated method stub
			camera = openFrontFacingCameraGingerbread();//Camera.open();
		}
		@TargetApi(Build.VERSION_CODES.GINGERBREAD)
		@SuppressLint("NewApi")
		private Camera openFrontFacingCameraGingerbread() {
		    int cameraCount = 0;
		    Camera cam = null;
		    Camera.CameraInfo cameraInfo = new Camera.CameraInfo();
		    cameraCount = Camera.getNumberOfCameras();
		    for (int camIdx = 0; camIdx<cameraCount; camIdx++) {
		        Camera.getCameraInfo(camIdx, cameraInfo);
		        if (cameraInfo.facing == Camera.CameraInfo.CAMERA_FACING_FRONT) {
		            try {
		                cam = Camera.open(camIdx);
		            } catch (RuntimeException e) {
		                Log.e("Your_TAG", "Camera failed to open: " + e.getLocalizedMessage());
		            }
		        }
		    }
		    return cam;
		}
		@Override
		public void surfaceDestroyed(SurfaceHolder holder) {
			// TODO Auto-generated method stub
            try {
                camera.stopPreview();
                camera.release();
                camera = null;
                previewing = false;
            }
            catch(Exception ex){
                 Toast.makeText(getApplicationContext(),
                        "Front Camera is required for this application.",
                        Toast.LENGTH_LONG).show();


            }
			
		}
	private String LoggedIn()
	{
		  
		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		String restoredText = prefs.getString("session_id", null);
		if (restoredText != null) 
		{
			try
			{
				Log.e("SessionName","sss");
				session_company_name = prefs.getString("session_company_name", null);
				Log.e("SessionName",session_company_name );
				session_admin_name = prefs.getString("session_admin_name", null);
				Log.e("SessionName",session_admin_name  );
			}catch(Exception ex){}
			return restoredText;
		}
		
		Intent intent = new Intent(home_menu.this, login.class);
		startActivity(intent);
  		finish();	            		
		return "";
	}
	private void MemberInfo()
	{
		try
		{
		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		Session_data  = prefs.getString("session_data", null);
		if (Session_data != null) 
		{
			if (!Session_data.trim().equals(""))
			{

				Log.e("tat memberinfo",Session_data );
				String[] MemberList = Session_data.split("@#@#@#");
				for(int i=0; i <= MemberList.length -1; i++ )
				{
					String[] DataList = MemberList[i].split("~");
					Model_MemberInfo mmi = new Model_MemberInfo(DataList[0],DataList[1],Integer.parseInt(DataList[2]));
					MemInfo.add(mmi);
					
				}
				
				return;
			}
			
		}
		
		}
		catch(Exception ex)
		{}
		
		
	}

    private int GetMemberCurrentState(String QRData)
    {
        if (Session_data.indexOf("~"+QRData+"~0") != -1)
            return 0;
        else if (Session_data.indexOf("~"+QRData+"~1") != -1)
            return 1;
        else
            return -1;
    }
    private int GetMemberCurrentState()
	{
		if (Session_data.indexOf("~"+EmployeeId+"~0") != -1)
			return 0;
		else if (Session_data.indexOf("~"+EmployeeId+"~1") != -1)
			return 1;
		else
			return -1;
	}
	public Model_MemberInfo GetMemberInfoFilter()
	{
		
		for (final Model_MemberInfo g : MemInfo) {
        		if (g.code.equals(EmployeeId))
                {
        			Log.e("EmployeeId",EmployeeId);
        			return g;
                }
        }
        	        	
        
		return null;
		
	}
	public int GetMemberInfoFilterPosition()
	{
		Log.e("GetMemberInfoFilterPosition()",EmployeeId);
		int pos = 0;
		for (final Model_MemberInfo g : MemInfo) {
        		if (g.code.equals(EmployeeId))
                {
        			
        			return pos;
                }
        		pos++;
        }
        	        	
		Log.e("GetMemberInfoFilterPosition()","Not found");
		return -1;
		
	}
	class Model_MemberInfo {
		String name = "";
		String code = "";
		int action = 0;
		public Model_MemberInfo(String Name, String Code, int Action)
		{
			this.name = Name;
			this.code = Code;
			this.action = Action;
		}
		public String SignedInMsg()
		{
            if (this.name == null){
                return " signed in";
            }
			return this.name  +  " has signed in";
		}
		public String SignedOutMsg()
		{
            if (this.name == null){
                return " signed out";
            }
			return this.name  +  " has signed out";
		}
	}
	class EmployeeTime
	{
		String Session_id = SessionID;
		String Employee_Id = EmployeeId;
		int hh = 0;
		int mm = 0;
		int dd = 0;
		int ampm = 0;
		int mo = 0;
		int yyyy = 0;
		int dow = 0;
		String CurrentDate = "";
		
	}

	private EmployeeTime FilterCalendarDateTime()
	{
		 Calendar cal = Calendar.getInstance(); 
		SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss"); //0000-00-00 00:00:00
     
        
		   String CurrentDate = df.format(cal.getTime());
          int dayofweek = cal.get(Calendar.DAY_OF_WEEK);
          int Hr24=cal.get(Calendar.HOUR);
          int Min60=cal.get(Calendar.MINUTE);
          int AMPM =cal.get(Calendar.AM_PM);
          int yyyy =cal.get(Calendar.YEAR);
          int mo =cal.get(Calendar.MONTH);
          int dd = cal.get(Calendar.DAY_OF_MONTH);
          
		 EmployeeTime ET = new EmployeeTime();
		 ET.hh = Hr24;
		 ET.mm = Min60;
		 ET.ampm = AMPM;
		 ET.dow = dayofweek;
		 ET.yyyy = yyyy;
		 ET.mo = mo;
		 ET.dd = dd;
		 ET.CurrentDate  = CurrentDate;
		 
		 return ET;
	}
	private void ActionInOut()
	{
		int State = GetMemberCurrentState();
		if (State == -1)
		{
			buttonIn.setVisibility(View.GONE);
		}
		else
		{
            CameraOn();
            if (State  == 0)
			{
					// In Message 
				buttonIn.setVisibility(View.VISIBLE);
				buttonIn.setText("In");
				
					
			}
			else if (State  == 1)
			{
				buttonIn.setVisibility(View.VISIBLE);
				buttonIn.setText("Out");
					
			}
			
			
		}
	}
	private void ResetCompanyInstName()
	{

		final Handler handler = new Handler();
	    Runnable runnable = new Runnable() {
	        
	        public void run() {
	                try {
	                    Thread.sleep(20000);
	                }    
	                catch (InterruptedException e) {
	                    e.printStackTrace();
	                }
	                handler.post(new Runnable(){
	                    public void run() {
                            tvHeaderTitle.setText(session_company_name);
	                    	imgViewCaptureImage.setVisibility(View.GONE);
	                    	SyncPicture();
	                }
	            });
	            
	        }
	    };
	    new Thread(runnable).start();		


	}
	public void ResetCompanyMsg(TextView tmp)
	{
		TitleAnimated();
        tvHeaderTitle.setText(session_company_name);
		
	
	}
	private void TitleAnimated()
	{
        tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.slide_out_right));
	}
	private void SaveDataMemory(int InOut)
	{	
		try {
            TakePicture();
            SaveSessionData(InOut);
            Model_MemberInfo mmi = GetMemberInfoFilter();
            int i = GetMemberInfoFilterPosition();
            if (i != -1) MemInfo.get(i).action = InOut;

            TitleAnimated();
            if (InOut == 0) // Out
            {
                tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.slide_in_left));

                tvHeaderTitle.setText(mmi.SignedInMsg());
            } else {
                tvHeaderTitle.startAnimation(AnimationUtils.loadAnimation(home_menu.this, android.R.anim.slide_out_right));

                tvHeaderTitle.setText(mmi.SignedOutMsg());
            }
            ResetCompanyInstName();
            EmployeeTime et = FilterCalendarDateTime();
		/* Format to store in memory  ********** IMPORTANT **********
		 * LocalString : Employee_id,Action,	dd,		mo,		yy,hh,		mm,		ampm,		dow,devicetime:
		 * Colon will be separator
		 */
            String SaveInMemory = GetPreferenceValue("SyncData");
            CurrentDateTime = et.CurrentDate.replace(":", "-"); // for image save filename
            CurrentDateTime = CurrentDateTime.replace(" ", ""); // for image save filename
            CurrentCode = et.Employee_Id;
            SaveInMemory += et.Employee_Id + ",";
            SaveInMemory += InOut + ",";
            SaveInMemory += et.dd + ",";
            SaveInMemory += et.mo + ",";
            SaveInMemory += et.yyyy + ",";
            SaveInMemory += et.hh + ",";
            SaveInMemory += et.mm + ",";
            SaveInMemory += et.ampm + ",";
            SaveInMemory += et.dow + ",";
            SaveInMemory += et.CurrentDate + ",";
            SaveInMemory += et.Session_id + ",";
            SaveInMemory += "$$";
            SavePreference("SyncData", SaveInMemory);
            Log.e("SyncData", SaveInMemory);
            DownloadWebPageTask task = new DownloadWebPageTask();
            task.execute(new String[]{"http://kent.nasz.us/elog_php/syncdata.php"});
        }catch(Exception ex){
            Toast.makeText(getApplicationContext(),
                    "Error in information " + ex.toString(),
                    Toast.LENGTH_LONG).show();

        }
	}
	private void SaveSessionData(int InOrOut)
	{
		
		if (Session_data != null) 
		{
			if (!Session_data.trim().equals(""))
			{
				Log.e("session_data before",Session_data );
				Log.e("session_data CurrentCode",CurrentCode);
				
				Log.e("InOrOut ",String.valueOf(InOrOut ));
				if (InOrOut == 0)
				{
					Session_data  = Session_data.replace("~"+EmployeeId+"~0", "~"+EmployeeId+"~1");
				}
				else 
				{
					Session_data  = Session_data.replace("~"+EmployeeId+"~1", "~"+EmployeeId+"~0");	
				}
				
			  SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
	    	  editor.putString("session_data", Session_data);
	    	  editor.commit();
	    	  Log.e("session_data after ",Session_data );
	    	  
			}
			
		}
	
	}
	private void EmployeeInButton()
	{
		if(buttonIn.getText().equals("In"))
			SaveDataMemory(0);
		else
			SaveDataMemory(1);	
		ClearCodeInText();
	}
	private void EmployeeResetButton()
	{
		ClearCodeInText();
	}
	private void IncrementXCodeInText(String sInput)
	{
		if (tvCodeEntered.getText().toString().trim().equals(""))
			tvCodeEntered.setText(sInput);
		else
			tvCodeEntered.setText(tvCodeEntered.getText().toString() + sInput);
			
		
	}
	private void ClearCodeInText()
	{
		buttonIn.setVisibility(View.GONE);
		tvCodeEntered.setText("");
		EmployeeId = "";	
	}
	private void OnBind()
	{
		Typeface myTypeface = Typeface.createFromAsset(this.getAssets(),"ds-digital-bold.ttf");
		tvCodeEntered.setTypeface(myTypeface);
        tvHeaderTitle.setTypeface(myTypeface);

		tvCodeEntered.addTextChangedListener(new TextWatcher() {

            public void afterTextChanged(Editable s) {

                EmployeeId = s.toString();

                ActionInOut();
            }

			@Override
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void onTextChanged(CharSequence s, int start, int before,
					int count) {
				// TODO Auto-generated method stub
				
			}
        });

		 button0 = (Button) findViewById(R.id.btn0);
		 button0.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("0");
	        	  }
	        });
		 button1 = (Button) findViewById(R.id.btn1); button1.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("1");
   	  		}
		 });
		 button2 = (Button) findViewById(R.id.btn2); button2.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("2");
	  		}
		 });
		 
		 button3 = (Button) findViewById(R.id.btn3); button3.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("3");
	  		}
		 });
		 
		 button4 = (Button) findViewById(R.id.btn4); button4.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("4");
	  		}
		 });
		 
		 button5 = (Button) findViewById(R.id.btn5); button5.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("5");
	  		}
		 });
		 
		 button6 = (Button) findViewById(R.id.btn6); button6.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("6");
	  		}
		 });
		 
		 button7 = (Button) findViewById(R.id.btn7); button7.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("7");
	  		}
		 });
		 
		 button8 = (Button) findViewById(R.id.btn8); button8.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("8");
	  		}
		 });
		 
		 button9 = (Button) findViewById(R.id.btn9); button9.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 IncrementXCodeInText("9");
	  		}
		 });
		 
		 buttonIn = (Button) findViewById(R.id.btnIn); buttonIn.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 EmployeeInButton();
	  		}
		 });
		 
		 
		 buttonReset = (Button) findViewById(R.id.btnReset); buttonReset.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
			 EmployeeResetButton();
	  		}
		 });

        buttonScanner = (Button) findViewById(R.id.btnscanner); buttonScanner.setOnClickListener(new View.OnClickListener() {@Override public void onClick(View view) {
        OpenScannerActivity();
    }
    });

		 
		 
		
	}
    private void OpenScannerActivity(){
        Intent intent = new Intent(this, CameraTestActivity.class);
        startActivity(intent);
    }
	private boolean AutoStartUp(String item)
	{
		if (item.equals("AutoStartUpMMLangauge"))
		{
			SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
			String restoredText = prefs.getString(item, null);
			if (restoredText == null)  // if its firsttime 
			{
				/** Save item ***/
				SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
				editor.putString(item,"true");
				editor.commit();
			
				
				CharSequence SearchArray[] = new CharSequence[] {"English", "Italian", "Dutch", "German", "French", "Spanish", "Portuguese"};

				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.setTitle("Choose color");
				builder.setItems(SearchArray, new DialogInterface.OnClickListener() {
				    @Override
				    public void onClick(DialogInterface dialog, int which) {
				        // the user clicked on colors[which]
				    	SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
						editor.putString("MMLang",String.valueOf(which));
						editor.commit();
						
			    		int[] values= new int[2];
				    	int x = 50;
				    	int y = 200;
		
				    	Toast prName = Toast.makeText(getApplicationContext(),
				                "You can change language again in setting.",
				                Toast.LENGTH_LONG);
				            prName.setGravity(Gravity.TOP|Gravity.LEFT, x, y);
				            prName.show();
					            
				    }
				});
				builder.show();
				 return true;
				
				
			}
		
		}
		 return false;
	
		
	}

		  

		 
    public String GetPreferenceValue(String StringName)
    {
    	SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		String restoredText = prefs.getString(StringName, null);
		if (restoredText != null) 
		{
			return restoredText;
		}
		return "0";
    }
  
    public void SetPreferenceValue(String StringName,String StringValue)
    {
    	try
    	{
    		int LastValue = Integer.valueOf(GetPreferenceValue(StringName));
    		int AddOnValue = Integer.valueOf(StringValue);
    		int TotalValue = LastValue + AddOnValue;
    		Log.e("Added Preference Value", String.valueOf(TotalValue));
    		StringValue =  String.valueOf(TotalValue);
    		SavePreference(StringName,StringValue);
    		
    	}
    	catch(Exception ex)
    	{
    		SavePreference(StringName,"1");
    	}
    }
   
    public void SavePreference(String StringName,String StringValue)
    {
    	
    	SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
		editor.putString(StringName,StringValue);
	    Log.e("SetPreferenceValue " + StringName, StringValue);
	    editor.commit();
    }
	
	private class DownloadWebPageTask extends AsyncTask<String, Context, String> {
		protected Context ctx;
		@Override
	    protected String doInBackground(String... urls) {
			
			Log.e("doInBackground","enter");
		      String response = "";
		      String uri = "";
		      for (String url : urls) {
		    	  uri = url;
		    	  Log.e("uri",uri);
		    	  try {
				        DefaultHttpClient client = new DefaultHttpClient();
				        HttpPost httpGet = new HttpPost(url);
				        try {
				        	Log.e("HttpPost", "Progress 0");
				        	  List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(1);
				        	  nameValuePairs.add(new BasicNameValuePair("SyncData", GetPreferenceValue("SyncData"))); // urls[1] = EmployeId,Action Of InOrOut,Time:EmployeId,Action Of InOrOut,Time:EmployeId,Action Of InOrOut,Time:EmployeId,Action Of InOrOut,Time:
					          Log.e("HttpPost", "Progress 1");
					          httpGet.setEntity(new UrlEncodedFormEntity(nameValuePairs));
					          Log.e("HttpPost", "Progress 2");
				          HttpResponse execute = client.execute(httpGet);
				          Log.e("HttpPost", "Progress 2.5");
				          InputStream content = execute.getEntity().getContent();
				          Log.e("HttpPost", "Progress 3");
				          BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
				          String s = "";
				          Log.e("HttpPost", "Progress 4");
				          while ((s = buffer.readLine()) != null) {
				            response += s;
				          }
			
				        } catch (Exception e) {
				        	 Log.e("Error http:", e.toString());
				        
				        	 e.printStackTrace();
				      		return "-999";
				        }
				  }
		    	  catch(Exception ex)
		    	  {
		    		  Log.e("Error http:", ex.toString());
		    		  return "-999";
		    	  }
		      }
	      Log.e("Response",response);
	     
	      return response;
	    }

	    @Override
	    protected void onPostExecute(String result) {
	    	if (result.equals("-999")) 
	    		{
	    			return;
	    		}
	    	
	    	ShowFeed(result);
	    	
	    }
	  }
	private void ShowFeed(String result)
	{
		Log.e("Response",result);
		
		
		
		if (result.length() >= 4)
		{
			if (result.trim().substring(0, 4).equals("2001")) // Update Member Data
			{
				RefreshMemberInfoCache(result.substring(4));
				return;
			}
		}
		if (result.trim().equals("1001")) // When async Reposonse received update successfully with value 1001 so clear old sych data
		{
			SavePreference("SyncData","");
			
		}
		
		else if (result.trim().equals("1002")) // Logout successfull
		{
			if (OpenAfterPasswordSuccessfull.equals("Activity"))
	        	{
					OpenAfterPasswordSuccessfull = "";
				 	ManageReportMemberList();
					return;
		//	//		Intent intent = new Intent(this, activity_member.class);
		///			intent.putExtra("Response",result);
		//			startActivity(intent);
					
					//openURL("http://kent.nasz.us/elog_php/manage.php?session_id="+SessionID);
	        	}
			if (OpenAfterPasswordSuccessfull.equals("LogOut"))
			{
					OpenAfterPasswordSuccessfull= "";
					SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
					editor.remove("session_id");
					editor.remove("lastUpdateTime");
					editor.remove("session_admin_name");
					editor.remove("session_data");
					editor.remove("session_company_name");
					
					
					editor.commit();
	    	  
		    	  
					Intent intent = new Intent(this, login.class);
					startActivity(intent);
					finish();
			}
		}
		else if (result.trim().equals("405")) // Logout successfull
		{
			Toast.makeText(getApplicationContext(), "Password is not correct.", Toast.LENGTH_SHORT).show();
			
		}
  	        
  	        
	}
	private void ManageReportMemberList()
	{
		
		Intent intent = new Intent(home_menu.this, activity_member_listactivity.class);
		startActivity(intent);
 		
	}
	private void RefreshMemberInfoCache(String MemberDataSession)
	{
		
	  if (MemberDataSession.equals(""))
	  {
		  Intent intent = new Intent(this, member.class);
			startActivity(intent);
			finish();
			return ;
	  }
		Log.e("Session_data RefreshMemberInfoCache",MemberDataSession);
  	  SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
  	  editor.putString("session_data", MemberDataSession);
  	  editor.commit();
  	  MemberInfo();
	}
	@Override
	 public boolean onCreateOptionsMenu(Menu menu) {
                MenuInflater inflater = getMenuInflater();
                inflater.inflate(R.menu.keypad_menu, menu);
                return true;
       }
	
	
	@SuppressLint("NewApi")
	private void UserThemeApply()
	{
		Log.e("UserThemeApply","Start");
		int i = 0;
		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0); 
		String restoredText = prefs.getString("ThemeSelected", null);
		if (restoredText == null)  // if its firsttime 
		{
			return;
		}
		else
		{
			i = Integer.parseInt(restoredText);
		}
		int idDraw = R.drawable.button_blue;
		
		switch (i) {
		case 0:
			idDraw = R.drawable.button_blue;
			break;
		case 1:
			idDraw = R.drawable.button_black;
			break;
		case 2:
			idDraw = R.drawable.button_green;
			break;

		case 3:
			idDraw = R.drawable.button_purple;
			break;
		case 4:
			idDraw = R.drawable.button_red;
			break;
		case 5:
			idDraw = R.drawable.button_yellow;
			break;

		default:
			break;
		}
		
		button0.setBackground(this.getResources().getDrawable(idDraw));
		button1.setBackground(this.getResources().getDrawable(idDraw));
		button2.setBackground(this.getResources().getDrawable(idDraw));
		button3.setBackground(this.getResources().getDrawable(idDraw));
		button4.setBackground(this.getResources().getDrawable(idDraw));
		button5.setBackground(this.getResources().getDrawable(idDraw));
		button6.setBackground(this.getResources().getDrawable(idDraw));
		button7.setBackground(this.getResources().getDrawable(idDraw));
		button8.setBackground(this.getResources().getDrawable(idDraw));
		button9.setBackground(this.getResources().getDrawable(idDraw));
		buttonIn.setBackground(this.getResources().getDrawable(idDraw));
	//	buttonReset.setBackground(this.getResources().getDrawable(idDraw));
		
		Log.e("UserThemeApply","End");
		
	}
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
	        case R.id.mnuChangeTheme:
	        	CharSequence SearchArray[] = new CharSequence[] {"Blue", "Black", "Green", "Purple", "Red", "Yellow"};
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.setTitle("Choose Color");
				builder.setItems(SearchArray, new DialogInterface.OnClickListener() {
				    @Override
				    public void onClick(DialogInterface dialog, int which) {
				        // the user clicked on colors[which]
				    	SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
						editor.putString("ThemeSelected",String.valueOf(which));
						editor.commit();
						UserThemeApply();
				    }
				});
				builder.show();
				return true;
	        
	        case R.id.mnuAddMember:
	        	Intent intent = new Intent(this, member.class);
      			startActivity(intent);
      			return true;
	        case R.id.mnuActivity:
	        	OpenAfterPasswordSuccessfull = "Activity";
	        	Dialog();
	        	return true;
	        case R.id.mnuLogout:
	        	OpenAfterPasswordSuccessfull = "LogOut";
	        	Dialog();
	        	return true;
        }
		return false;
    }
	private void openURL(String URL)
	{
		
		  Intent intent = new Intent(home_menu.this, activity_events.class);
		  intent.putExtra("url", URL);
		  startActivity(intent);
		  
	}
	public void Dialog()
	{
		String Action = "Enter Admin Password";//((TextView) view.findViewById(R.id.lv_tv_note)).getText().toString();
			
			 
			final AlertDialog.Builder alert = new AlertDialog.Builder(ctx);
			final EditText input = new EditText(ctx);
			input.setHint(Action);
			alert.setTitle( "Admin Password??" );
		    alert.setView(input);
		    alert.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
		        public void onClick(DialogInterface dialog, int whichButton) {
		            String value = input.getText().toString().trim();
		      	  	DownloadWebPageTask task = new DownloadWebPageTask();
		      	  	task.execute(new String[] { "http://kent.nasz.us/elog_php/passwordverifysession.php?session_id="+SessionID+ "&password="+value});
		            
		        }
		    });

		    alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
		        public void onClick(DialogInterface dialog, int whichButton) {
		            dialog.cancel();
		        }
		    });
		    alert.show();  
		
	}
	
	private void SyncPicture()
	{
	//	new ImageGalleryTask().execute();
		 File currentDir  = new File(Environment.getExternalStorageDirectory()
                 + File.separator +"shifa_elogger"+ File.separator + "img");  
		File[] dirs = currentDir.listFiles();  
		try
		{
			for (File ff : dirs) {  
			    String name = ff.getName();
			    Log.e("Filename",name);
			    new ImageGalleryTask().execute(new String[] { name});
			}
		}
		catch (Exception e) {
			Log.e("SycnPicture",e.toString());
			// TODO: handle exception
		}
	}
	
	private class ImageGalleryTask extends AsyncTask<String, Context, String> {
		protected Context ctx;
		@Override
		protected String doInBackground(String... urls) {
			try{
				InputStream is;
			    BitmapFactory.Options bfo;
			    Bitmap bitmapOrg;
			    ByteArrayOutputStream bao ;
			   
			    bfo = new BitmapFactory.Options();
			    bfo.inSampleSize = 5;
			    bitmapOrg = BitmapFactory.decodeFile(Environment.getExternalStorageDirectory()
		                 + File.separator +"shifa_elogger"+ File.separator + "img" + File.separator  + urls[0], bfo);
			      
			    bao = new ByteArrayOutputStream();
			    bitmapOrg.compress(Bitmap.CompressFormat.JPEG, 90, bao);
				byte [] ba = bao.toByteArray();
				String ba1 = Base64.encodeBytes(ba);
				ArrayList<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>();
				nameValuePairs.add(new BasicNameValuePair("image",ba1));
				nameValuePairs.add(new BasicNameValuePair("cmd","image_android"));
				nameValuePairs.add(new BasicNameValuePair("session_id",urls[0]));
				Log.v("log_tag", System.currentTimeMillis()+".jpg");	       
				
				        HttpClient httpclient = new DefaultHttpClient();
				        HttpPost httppost = new 
                      //  Here you need to put your server file address
				        HttpPost("http://kent.nasz.us/elog_php/img/imageupload.php");
				        httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
				        HttpResponse response = httpclient.execute(httppost);
				        HttpEntity entity = response.getEntity();
				        is = entity.getContent();
				        Log.v("log_tag", "In the try Loop" );
				        return urls[0];
				   }catch(Exception e){
				        Log.v("log_tag", "Error in http connection "+e.toString());
				   }
			return null;
			// (null);
		}
		protected void onPostExecute(String result) {
	    	if (result == null) return;
	    	try
	    	{
	    		File file = new File(Environment.getExternalStorageDirectory()
		                + File.separator +"shifa_elogger"+ File.separator + "img" + File.separator  + result);
		    	
		    	file.delete();
	    	}
	    	catch(Exception ex){}
	    	
	    }
	}
	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		   if (keyCode == KeyEvent.KEYCODE_BACK) {
			   openOptionsMenu();
			   
		        return true;
		    }
	    return super.onKeyDown(keyCode, event);
	}
	/* APP Update checkavailble */
    private void DayChanged()
    {

    	/* Get Last Update Time from Preferences */
		SharedPreferences prefs = getSharedPreferences("AppNameSettings",0);
		long  lastUpdateTime = prefs.getLong("lastUpdateTime", 0);
		
		/* Should Activity Check for Updates Now? */
		if ((lastUpdateTime + (24 * 60 * 60 * 1000)) < System.currentTimeMillis() || (checkNewLogin() == true) ) {

		    /* Save current timestamp for next Check*/
		    lastUpdateTime = System.currentTimeMillis();            
		    SharedPreferences.Editor  editor  = getSharedPreferences("AppNameSettings",0).edit();
		    editor.putLong("lastUpdateTime", lastUpdateTime);
		    editor.commit();        
		    Log.e("Fresh request","in");
		    DownloadWebPageTask task = new DownloadWebPageTask();
			task.execute(new String[] { "http://kent.nasz.us/elog_php/memberdatasession.php?session_id="+SessionID});
		    
		    /* 24hour Day changed 
		     * Reset all the in status
		     * */
		    
		   
		}
		else
		{
			Log.e("Cancel Fresh","Timeover");
			
			return;
		}
    }


    @Override
    protected void onStart() {
        super.onStart();
		//Check is it date changed if yes so reset all the keycode and start it new session
		 DayChanged();
     
    }


    /** A safe way to get an instance of the Camera object. */
    public static Camera getCameraInstance(){
        Camera c = null;
        try {
            c = Camera.open();
        } catch (Exception e){
        }
        return c;
    }

    private void releaseCamera() {
        if (mCamera != null) {
            previewing = false;
            mCamera.setPreviewCallback(null);
            mCamera.release();
            mCamera = null;
        }
    }

    private Runnable doAutoFocus = new Runnable() {
        public void run() {
            if (previewing)
                mCamera.autoFocus(autoFocusCB);
        }
    };

    Camera.PreviewCallback previewCb = new Camera.PreviewCallback() {
        public void onPreviewFrame(byte[] data, Camera camera) {
            Camera.Parameters parameters = camera.getParameters();
            Camera.Size size = parameters.getPreviewSize();

            Image barcode = new Image(size.width, size.height, "Y800");
            barcode.setData(data);

            int result = scanner.scanImage(barcode);

            if (result != 0) {
                //previewing = false;
                //mCamera.setPreviewCallback(null);
                //mCamera.stopPreview();

                SymbolSet syms = scanner.getResults();
                for (Symbol sym : syms) {
                    //barcode result " +
                    if (GetMemberCurrentState(sym.getData()) != -1) {
                        tvHeaderTitle.setText(sym.getData());
                        tvCodeEntered.setText(sym.getData());
                        EmployeeId = sym.getData();
                        TimerGetSetGo(4);

                    }
                }
    //
  //              mCamera.takePicture(myShutterCallback,myPictureCallback_RAW, myPictureCallback_JPG);
//                EmployeeInButton();
            }
        }
    };

    private void TimerGetSetGo(final int LoopCount){


        final Handler handler = new Handler();
        Runnable runnable = new Runnable() {

            public void run() {
                int LoopUntil = LoopCount;
                while(LoopUntil != 0) {
                    try {
                        LoopUntil--;
                        Thread.sleep(1000);
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                    final int CurrentLoop = LoopUntil;
                    handler.post(new Runnable() {
                        public void run() {
                            GetSetGo(CurrentLoop);
                        }
                    });
                }
            }
        };

        new Thread(runnable).start();
    }

    private void GetSetGo(int TimerCountDown){
        if (TimerCountDown == 0)
        {

            EmployeeInButton();
        }else if (TimerCountDown == 1){
            tvHeaderTitle.setText("Get Ready For Picture");
            //tvHeaderTitle.setText(""+TimerCountDown);
        }else if (TimerCountDown == 2){
            tvHeaderTitle.setText("Ready: "+TimerCountDown);
            //tvHeaderTitle.setText(""+TimerCountDown);
        }
        else
        {
            tvHeaderTitle.setText("Ready: "+TimerCountDown);
            tvHeaderTitle.setVisibility(View.VISIBLE);
        }
    }
    // Mimic continuous auto-focusing
    Camera.AutoFocusCallback autoFocusCB = new Camera.AutoFocusCallback() {
        public void onAutoFocus(boolean success, Camera camera) {
            autoFocusHandler.postDelayed(doAutoFocus, 1000);
        }
    };
    public void onPause() {
        super.onPause();
        releaseCamera();
    }
    private Camera mCamera;
    private CameraPreview mPreview;
    private Handler autoFocusHandler;

    Button scanButton;
    FrameLayout previewMain;
    ImageScanner scanner;

    private boolean barcodeScanned = false;
   // private boolean previewing = true;
    private void onBindQRCode(){
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        autoFocusHandler = new Handler();
        mCamera = openFrontFacingCameraGingerbread();//getCameraInstance();

        /* Instance barcode scanner */
        scanner = new ImageScanner();
        scanner.setConfig(0, Config.X_DENSITY, 3);
        scanner.setConfig(0, Config.Y_DENSITY, 3);

        mPreview = new CameraPreview(this, mCamera, previewCb, autoFocusCB);
        previewMain  = (FrameLayout)findViewById(R.id.cameraPreview);
        previewMain.addView(mPreview);

        scanButton = (Button)findViewById(R.id.btnscanner);
        scanButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if (barcodeScanned) {

                    CameraScannerOn();
                }
                else{
                    CameraScannerOff();
                }
            }
        });


     //   TimerGetSetGo(5);

    }
    private void CameraScannerOn(){
        barcodeScanned = false;
        if (mCamera != null) {
            mCamera.setPreviewCallback(previewCb);
            mCamera.startPreview();
            mCamera.autoFocus(autoFocusCB);
        } previewMain.setVisibility(View.VISIBLE);
        imgViewCaptureImage.setVisibility(View.GONE);
        previewing = true;
        scanButton.setText("Card Scanner Off");
        tvHeaderTitle.setText("Scanner Mode");
        //     KeyPadOff();

       }

    private void CameraScannerOff(){
        barcodeScanned = true;
        previewing = false;
        if (mCamera != null) {
            mCamera.setPreviewCallback(null);
            mCamera.stopPreview();
        }
        previewMain.setVisibility(View.GONE);
       // KeyPadOn();
        imgViewCaptureImage.setVisibility(View.GONE);
        scanButton.setText("Card Scanner On");
        tvHeaderTitle.setText("KeyPad Mode");

    }
}
