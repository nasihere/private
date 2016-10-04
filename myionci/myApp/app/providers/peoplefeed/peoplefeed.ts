import {Injectable, Inject} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class Peoplefeed {
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
        "type": "following",
        "name": "Stan Shaul",
        "nickname": "ptsofware",
        "icon": "build/images/stanshaul-sm.jpg",
        "age": "39",
        "city": "Newbury Park",
        "country": "USA",
        "challengedyet": false,
        "follower": true,
        "following": true
      },
      {
        "type": "following",
        "name": "Bob Barker",
        "nickname": "bbark",
        "icon": "build/images/dzabriske-sm.jpg",
        "age": "78",
        "city": "Atlanta",
        "country": "USA",
        "challengedyet": true,
        "follower": false,
        "following": true
      },
      {
        "type": "suggested",
        "name": "Linda Veros",
        "nickname": "lveros",
        "icon": "build/images/joebuchanan-sm.jpg",
        "age": "35",
        "city": "Austin",
        "country": "USA",
        "challengedyet": false,
        "follower": false,
        "following": false
      },
      {
        "type": "followers",
        "name": "Robert Paulsen",
        "nickname": "robbyp",
        "icon": "build/images/mallen-sm.jpg",
        "age": "27",
        "city": "New York",
        "country": "USA",
        "challengedyet": true,
        "follower": true,
        "following": false
      },
      {
        "type": "followers",
        "name": "Lisa Allan",
        "nickname": "robbyp",
        "icon": "build/images/mallen-sm.jpg",
        "age": "27",
        "city": "Burbank",
        "country": "USA",
        "challengedyet": true,
        "follower": true,
        "following": true
      }
    ]
  }
}

