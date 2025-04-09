import { ChangeDetectionStrategy, Component } from '@angular/core';
import type { ICellRendererAngularComp } from 'ag-grid-angular';
import type { ICellRendererParams } from 'ag-grid-community';


@Component({
  selector: 'app-action-buttons',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './action-buttons.component.html',
  styleUrl: './action-buttons.component.css'
})

export class ActionButtonsComponent {
  params: any;

  agInit(params: any): void {
    this.params = params;
  }

  invokeAction() {
    if (this.params.onClick) {
      this.params.onClick(this.params);
    }
  }

  invokeDelete() {
    if (this.params.onDelete) {
      this.params.onDelete(this.params);
    }
  }
}
