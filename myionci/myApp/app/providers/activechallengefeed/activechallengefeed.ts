import {Injectable, Inject} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class ActiveChallengeFeed {
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

    var activechallenge =  {
      duration: "3 Days, 6 Hours, 50 Minutes",
      title: "Haute-Normandie",
      activity: "cycling",
      description: "25 Miles, Long Beach California",
      reward: 300,
      isleading: true,
      thisuser: {
        name: "You",
        icon: "build/images/joebuchanan-sm.jpg",
        totaldistance: 395,
        favdistance: 5,
        averagespeed: 7,
        maxspeed: 12,
        elevation: 0
      },
      challenger: {
         name: "Brandon Turner",
         icon: "build/images/mallen-sm.jpg",
         totaldistance: 400,
         favdistance: 5,
         averagespeed: 8,
         maxspeed: 16,
         elevation: 0
      }
    }

    return activechallenge;

  }
}

