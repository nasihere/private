package com.diechart.adapter;

import java.util.List;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import com.diechart.diecharrt.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseExpandableListAdapter;
import android.widget.TextView;

public class ListAdapter extends ArrayAdapter<JSONObject> {

	private JSONArray jArray;
	private List<JSONObject> list;
	private LayoutInflater inflater;

	public ListAdapter(Context context, List list) {
		super(context, 0, list);
		// TODO Auto-generated constructor stub
		this.list = list;
		inflater = (LayoutInflater) context
				.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}

	@Override
	public int getCount() {
		return list.size();
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@SuppressWarnings("null")
	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO Auto-generated method stub
		ViewHolder holder;
		View v = convertView;
		if (v == null) {
			holder = new ViewHolder();
			v = inflater.inflate(R.layout.list_item1, null);
			holder.setCode((TextView) v.findViewById(R.id.code_val));
			holder.setDescription((TextView) v.findViewById(R.id.desc_val));
			holder.setId1((TextView) v.findViewById(R.id.id1_val));
			holder.setId2((TextView) v.findViewById(R.id.id2_val));
			holder.setLength((TextView) v.findViewById(R.id.len_val));
			holder.setPrice_val((TextView) v.findViewById(R.id.priceval));
			holder.setGlass_size((TextView) v.findViewById(R.id.glass_val));
			holder.setId1_id2((TextView) v.findViewById(R.id.id1_id2_val));
			v.setTag(holder);
		} else {
			holder = (ViewHolder) v.getTag();
		}
		try {
			holder.getCode().setText(list.get(position).optString("code"));
			holder.getDescription().setText(
					list.get(position).optString("desc"));
			holder.getId1().setText(list.get(position).optString("id1"));
			holder.getId2().setText(list.get(position).optString("id2"));
			holder.getLength().setText(list.get(position).optString("length"));
			holder.getPrice_val().setText(
					list.get(position).optString("price_val"));
			holder.getGlass_size().setText(
					list.get(position).optString("glass_size"));
			holder.getId1_id2()
					.setText(list.get(position).optString("id1_id2"));

		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return v;
	}
}
