import {Injectable, Inject} from 'angular2/core';
import {Http} from 'angular2/http';

@Injectable()
export class Challengesfeed {
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
        "itemtype": "challenge",
        "challengestate": "open",
        "name": "David Zabriske",
        "nickname": "DZib",
        "icon": "build/images/dzabriske-sm.jpg",
        "reward": "300",
        "start": "01-01-2016",
        "end": "12-31-2016",
        "type": "cycling",
        "description": "25 mile time trial"
      },
      {
        "itemtype": "completed",
        "challengestate": "done",
        "name_win": "Joe Buchanan",
        "nickname_win": "JBuck",
        "icon_win": "build/images/joebuchanan-sm.jpg",
        "name_lose": "Peter Sagan",
        "nickname_lose": "psagan",
        "icon_lose": "build/images/psagan-sm.jpg",
        "reward": "10000",
        "start": "01-01-2016",
        "end": "12-31-2016",
        "type": "cycling",
        "description": "fastest 20k"
      },
      {
        "itemtype": "inprogress",
        "challengestate": "active",
        "name": "Paul Bryan",
        "nickname": "PBryan",
        "icon": "build/images/default-sm.jpg",
        "name_from": "Stan Shaul",
        "nickname_from": "ptsoftware",
        "icon_from": "build/images/stanshaul-sm.jpg",
        "reward": "250",
        "start": "04-01-2016",
        "end": "04-30-2016",
        "type": "cycling",
        "description": "300 miles in a month"
      },
      {
        "itemtype": "inprogress",
        "challengestate": "active",
        "name": "Stan Shaul",
        "nickname": "ptsoftware",
        "icon": "build/images/stanshaul-sm.jpg",
        "name_from": "Joe Buchanan",
        "nickname_from": "JBuck",
        "icon_from": "build/images/joebuchanan-sm.jpg",
        "reward": "300",
        "start": "04-01-2016",
        "end": "05-31-2016",
        "type": "running",
        "description": "fastest 10k"
      },
      {
        "itemtype": "won",
        "challengestate": "done",
        "name": "Mark Allen",
        "nickname": "mallen",
        "icon": "build/images/mallen-sm.jpg",
        "reward": "300",
        "start": "01-01-2016",
        "end": "12-31-2016",
        "type": "running",
        "description": "fastest 10k"
      },
      {
        "itemtype": "lost",
        "challengestate": "done",
        "name": "Mirinda Carfrae",
        "nickname": "mc",
        "icon": "build/images/mcarfrae-sm.jpg",
        "reward": "350",
        "start": "01-01-2016",
        "end": "12-31-2016",
        "type": "cycling",
        "description": "fastest 40k"
      }
    ]
  }
}

