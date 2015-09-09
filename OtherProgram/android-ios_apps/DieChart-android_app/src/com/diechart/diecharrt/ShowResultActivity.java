package com.diechart.diecharrt;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.diechart.adapter.ListAdapter;
import com.diechart.database.DatabaseHandler;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;

import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

public class ShowResultActivity extends BaseActivity implements OnClickListener {

	ListAdapter listAdapter;
	ListView expListView;
	HashMap<String, List<String>> listDataHeader;
	HashMap<String, List<String>> listDataChild;
	private List<JSONObject> list;
	private List<JSONObject> searchList;
	private Button search_but;
	private EditText search_field;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.showactivity_layout);
		// get the listview

		expListView = (ListView) findViewById(R.id.listView1);
		DatabaseHandler db = new DatabaseHandler(getApplicationContext());
		String result = db.fetchAllrow();
		search_but = (Button) findViewById(R.id.button1);
		search_field = (EditText) findViewById(R.id.editText1);
		// search_field.setOnClickListener(this);

		search_but.setOnClickListener(this);
		try {
			populatelist(new JSONArray(result));
		} catch (JSONException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}

		listAdapter = new ListAdapter(this, list);

		// setting list adapter
		expListView.setAdapter(listAdapter);
	}

	private void populatelist(JSONArray jArray2) {
		// TODO Auto-generated method stub
		list = new ArrayList<JSONObject>();
		for (int i = 0; i < jArray2.length(); i++) {
			JSONObject j = null;
			try {
				j = jArray2.getJSONObject(i);
				list.add(j);
			} catch (JSONException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

		}

	}

	@Override
	public void onClick(View v) {
		// TODO Auto-generated method stub
		switch (v.getId()) {
		case R.id.button1:
			InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);

			inputManager.hideSoftInputFromWindow(getCurrentFocus()
					.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
			String search_key = search_field.getText().toString();
			if (search_key.length() == 0) {
				listAdapter = new ListAdapter(this, list);

				// setting list adapter
				expListView.setAdapter(listAdapter);
				listAdapter.notifyDataSetChanged();
			}
			searchFromJson(search_key);

			break;

		default:
			break;
		}

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// TODO Auto-generated method stub
		getMenuInflater().inflate(R.menu.main, menu);
		return false;
	}

	@Override
	public boolean onMenuItemSelected(int featureId, MenuItem item) {
		// TODO Auto-generated method stub
		return super.onMenuItemSelected(featureId, item);
	}

	private void searchFromJson(String search_key) {
		// TODO Auto-generated method stub
		searchList = new ArrayList<JSONObject>();
		for (int i = 0; i < list.size(); i++) {
			String key = list.get(i).toString();
			if (key.contains(search_key)) {
				searchList.add(list.get(i));
			}

		}
		listAdapter = new ListAdapter(this, searchList);

		// setting list adapter
		expListView.setAdapter(listAdapter);
		listAdapter.notifyDataSetChanged();
		System.out.println(searchList);

	}

}
