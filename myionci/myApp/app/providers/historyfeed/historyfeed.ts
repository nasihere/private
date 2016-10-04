import {Injectable, Inject} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class Historyfeed {
  // Declare globals
  public http: any;
  public data: any;
  constructor(private httpInfo: Http) {
    // Setup
    this.http = httpInfo;
    this.data = null;
  }

  // Keep these for when we actually make an API request for data
  load() {
    if (this.data) {
      // already loaded data
      return Promise.resolve(this.data);
    }

    // don't have the data yet
    return new Promise(resolve => {
      // We're using Angular Http provider to request the data,
      // then on the response it'll map the JSON data to a parsed JS object.
      // Next we process the data and resolve the promise with the new data.
      this.http.get('path/to/data.json')
        .map(res => res.json())
        .subscribe(data => {
          // we've got back the raw data, now generate the core schedule data
          // and save the data for later reference
          this.data = data;
          resolve(this.data);
        });
    });
  }

  // Get data from a dummy data set
  loadResults() {
    return [
      {
        "historytype": "won",
        "iconURL": "build/images/dzabriske-sm.jpg",
        "icon": "build/images/joebuchanan-sm.jpg",
        "name": "Brandon Turner",
        "type": "running",
        "description": "5 Miles, Long Beach California",
        "date": "1 - 24 - 16",
        "reward": "2500"
      },
      {
        "historytype": "lost",
        "iconURL": "build/images/dzabriske-sm.jpg",
        "icon": "build/images/mallen-sm.jpg",
        "name": "Brandon Turner",
        "type": "running",
        "description": "5 Miles, Long Beach California",
        "date": "1 - 24 - 16",
        "reward": "2500"
      },
      {
        "historytype": "purchase",
        "date": "1 - 17 - 16",
        "reward": "3000"
      },
      {
        "historytype": "gifted",
        "name": "Brandon Turner",
        "icon": "build/images/mallen-sm.jpg",
        "date": "3 - 24 - 16",
        "reward": "4000"
      }
    ]
  }

}

