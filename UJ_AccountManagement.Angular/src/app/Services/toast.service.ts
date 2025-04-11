declare var bootstrap: any;  // Declare the global bootstrap object

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToastService {

  constructor() { }

  showToast(type: 'success' | 'error' | 'warning', message: string, title: string = 'Notification'): void {
    const toastContainer = document.getElementById('toastPlacement');
    const toastHTML = `
      <div class="toast align-items-center text-bg-${type} border-0" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
          <!-- Toast Header -->
          <div class="toast-header">
            <strong class="me-auto">${title}</strong>
            <button type="button" class="btn-close btn-close-red" data-bs-dismiss="toast" aria-label="Close"></button>
          </div>
          <!-- Toast Body -->
          <div class="toast-body">
            ${message}
          </div>
        </div>
      </div>
    `;

    // Append the toast HTML to the container
    if (toastContainer) {
      toastContainer.innerHTML += toastHTML;

      // Initialize Bootstrap Toast and show it
      const toastElement = toastContainer.lastElementChild as HTMLElement;
      const toast = new bootstrap.Toast(toastElement);
      toast.show();

      // Remove the toast from the DOM after it fades out
      setTimeout(() => {
        toastContainer.removeChild(toastElement);
      }, 5000); // 5 seconds timeout
    }
  }
}
