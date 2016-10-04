import {Injectable, Inject} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class Rewardsfeed {
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
        "rewardtype": "purchase",
        "price": "20",
        "currency": "Dollars",
        "currencysymbol": "$",
        "points": "2000",
        "icon": "build/images/dzabriske-sm.jpg",
        "description": "Podiim Points Purchase",
        "savingspercent": "0"
      },
      {
        "rewardtype": "purchase",
        "price": "36",
        "currency": "Dollars",
        "currencysymbol": "$",
        "points": "4000",
        "icon": "build/images/dzabriske-sm.jpg",
        "description": "Podiim Points Purchase",
        "savingspercent": "10"
      },
      {
        "rewardtype": "redeem",
        "points": "1000",
        "icon": "build/images/amazoncard.png",
        "description": "$10 Amazon Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "1000",
        "icon": "build/images/starbuckscard.png",
        "description": "$10 Starbucks Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "1000",
        "icon": "build/images/targetcard.png",
        "description": "$10 Target Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "2500",
        "icon": "build/images/amazoncard.png",
        "description": "$25 Amazon Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "2500",
        "icon": "build/images/starbuckscard.png",
        "description": "$25 Starbucks Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "2500",
        "icon": "build/images/targetcard.png",
        "description": "$25 Target Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "5000",
        "icon": "build/images/amazoncard.png",
        "description": "$50 Amazon Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "5000",
        "icon": "build/images/starbuckscard.png",
        "description": "$50 Starbucks Gift Card"
      },
      {
        "rewardtype": "redeem",
        "points": "5000",
        "icon": "build/images/targetcard.png",
        "description": "$50 Target Gift Card"
      }
    ]
  }

  // Get data from a dummy data set
  loadUserStatus() {
    return {
        "activechallenges": "2",
        "allocatedpoints": "1000",
        "availablepoints": "25,000"
      }
  }

}

