import { Component } from '@angular/core';
import { SignalRService } from '../services/signal-r.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private observation: string;
  private observationSubscription: Subscription;

  constructor(public signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addObservationListener();
    this.observationSubscription = this.signalRService.observation.subscribe(
      observation => {
        this.observation = observation;
      }
    );
  }
}
