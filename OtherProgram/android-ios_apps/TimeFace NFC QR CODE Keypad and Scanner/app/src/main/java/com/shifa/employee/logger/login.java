package com.shifa.employee.logger;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.DefaultHttpClient;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;

public class login extends Activity{

	ProgressDialog progressDialog;
	boolean onlybackgroundthread = false;
	Context ctx;
	public String SessionID = "";
	

	@SuppressLint("NewApi")
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		ctx = this;
		LoginCreate();
				
		

	}
   
	    
	    
	    private void LoginCreate()
		{
			
			setContentView(R.layout.login);

            try
            {
                Bundle extras = getIntent().getExtras();
                String SessionID = "";
                if (extras != null) {
                    String emailid = extras.getString("emailid");
                    String password = extras.getString("password");
                    progressDialog =  ProgressDialog.show(login.this, "",
                            "Connecting internet... Please wait", true);
                    onlybackgroundthread = false;
                    DownloadWebPageTask task = new DownloadWebPageTask();
                    task.execute(new String[] { "http://kent.nasz.us/elog_php/login.php?email="+emailid+"&password="+ password});
                    return;
                }
            }
            catch(Exception ex)
            {
            }

	        final Button button = (Button) findViewById(R.id.btnSignin);
	        button.setOnClickListener(new View.OnClickListener() {

	        	  @Override
	        	  public void onClick(View view) {
	        		  
	        		  EditText edtemailid = (EditText) findViewById(R.id.edtLoginEmailId);
	        		  EditText edtpass = (EditText) findViewById(R.id.edtLoginpassword);
	        		  	                   
	        		  progressDialog =  ProgressDialog.show(login.this, "", 
	        				  "Connecting internet... Please wait", true);
	        		  onlybackgroundthread = false;
	        		  DownloadWebPageTask task = new DownloadWebPageTask();
	        		  task.execute(new String[] { "http://kent.nasz.us/elog_php/login.php?email="+edtemailid.getText().toString()+"&password="+ edtpass.getText().toString()});
	        		  
	        		 
	        	  }

	        	});
	        
	        
	        
	        final TextView textview1 = (TextView) findViewById(R.id.view_home_chat);
	        textview1.setOnClickListener(new View.OnClickListener() {

	        	  @Override
	        	  public void onClick(View view) {
	        		  

	      			Intent intent = new Intent(login.this, registration.class);
	      			
	      			startActivity(intent);
	            		
	            		
	        		 
	        	  }

	        	});

	        final TextView tvLoginForgotPassword = (TextView) findViewById(R.id.tvLoginForgotPassword);
	        tvLoginForgotPassword.setOnClickListener(new View.OnClickListener() {

	        	  @Override
	        	  public void onClick(View view) {
	        		  

	        		  Dialog();
	            		
	            		
	        		 
	        	  }

	        	});
	        
	
	        
	        
	        
		}
		
		public void Dialog()
		{
			String Action = "nasiXXXX@gmail.com";//((TextView) view.findViewById(R.id.lv_tv_note)).getText().toString();
				
				 
				final AlertDialog.Builder alert = new AlertDialog.Builder(ctx);
				final EditText input = new EditText(ctx);
				input.setText(Action);
				alert.setTitle( "Enter your email address." );
			    alert.setView(input);
			    alert.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
			        public void onClick(DialogInterface dialog, int whichButton) {
			            String value = input.getText().toString().trim();
			      	  progressDialog =  ProgressDialog.show(login.this, "", 
	                          "Connecting internet... Please wait", true);
			      	  	onlybackgroundthread = false;
	        		  		DownloadWebPageTask task = new DownloadWebPageTask();
			            	task.execute(new String[] { "http://kent.nasz.us/elog_php/forgotpassword.php?email="+value});
			            
			        }
			    });

			    alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
			        public void onClick(DialogInterface dialog, int whichButton) {
			            dialog.cancel();
			        }
			    });
			    alert.show();  
			
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
					        HttpGet httpGet = new HttpGet(url);
					        try {
					          HttpResponse execute = client.execute(httpGet);
					          InputStream content = execute.getEntity().getContent();
				
					          BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
					          String s = "";
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
		      Log.e("Response data background",response);
		     
		      return response;
		    }

		    @Override
		    protected void onPostExecute(String result) {
		    	 progressDialog.dismiss();
		    	if (result.equals("-999")) return;
		    	try
		    	{
		    	
		    	 
		    	  Log.e("Response result Data",result);
		    	  String login = result.trim(); 
		    	 
		    	  Log.e("Response Data",login);
		    	  if (login.equals("-1"))
			      {
			    	  Toast.makeText(getApplicationContext(), "Email id and password invalid.", Toast.LENGTH_SHORT).show();
			      }
		    	  else if (login.indexOf("-101-") != -1)
			      {
		    		  Log.e("Response Condition","-=-101-=-");
		    		  String[] data = login.split("-:-"); // Login Should return = "-=-101-=--:-Password Sent you by email."
		    		  Log.e("Response Condition","Split");
			    	  Toast.makeText(getApplicationContext(), data[1], Toast.LENGTH_SHORT).show();
			      }
			      else
			      { 
			    	  
			    	  String[] tmp =  login.split(":");

			    	  SharedPreferences.Editor editor = getSharedPreferences("AppNameSettings",0).edit();
			    	  editor.putString("session_id", tmp[0]);
			    	  editor.putString("session_company_name", tmp[1]);
			    	  editor.putString("session_admin_name", tmp[2]);
                      editor.putString("justlogin", "true");
                      editor.commit();
			    	  
		    	  
					Intent intent = new Intent(login.this, home_menu.class);
					startActivity(intent);
		      		finish();
			      }
		    	}
		    	catch(Exception ex)
		    	{
		    		//Toast.makeText(getApplicationContext(), ex.toString(), Toast.LENGTH_SHORT).show();
		    		
		    	}
		    	
		    }
		  }
		private void openURL(String URL)
		{
						  
		}
		
}
