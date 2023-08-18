<script lang="ts">
  import { AddNetworkDeviceRequest, NetworkClient } from "../../nlp-api";

  let request: AddNetworkDeviceRequest = new AddNetworkDeviceRequest();
  let canAdd: boolean = false;
  let info: string = '';
  let error: string = '';

  const validate = () => {
    canAdd = false;
    if(request.deviceName.length < 2) return;
    canAdd = true;
  };

  const resetRequest = () => {
    request = new AddNetworkDeviceRequest({
      deviceName: '',
      isPhysical: false,
    });
  };
  
  const addDevice = async () => {
    const response = await new NetworkClient().addDevice(request);
    if(response.success) {
      resetRequest();
      info = 'Device added successfully';
      setTimeout(() => { info = ''; }, 2000);
    } else {
      error = response.error || '';
      setTimeout(() => { error = ''; }, 2000);
    }
  };

  resetRequest();
</script>

<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addDevice">
  Add Device
</button>

<div class="modal fade" id="addDevice" tabindex="-1" aria-labelledby="addDeviceLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="addDeviceLabel">Add Device</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if info}<div class="alert alert-success" role="alert">{info}</div>{/if}
        {#if error}<div class="alert alert-danger" role="alert">{error}</div>{/if}
        <form class="row g-3">
          <div class="col-md-6">
            <label for="name" class="form-label">Name</label>
            <input type="text" class="form-control" id="name" bind:value={request.deviceName} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="floor" class="form-label">Floor</label>
            <input type="text" class="form-control" id="floor" bind:value={request.floor} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="room" class="form-label">Room</label>
            <input type="text" class="form-control" id="room" bind:value={request.room} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="roomLocation" class="form-label">Room Location</label>
            <input type="text" class="form-control" id="roomLocation" bind:value={request.roomLocation} on:keyup={validate}>
          </div>
          <div class="form-check form-switch">
            <input class="form-check-input" type="checkbox" role="switch" id="isPhysical" bind:checked={request.isPhysical} on:change={validate}>
            <label class="form-check-label" for="isPhysical">Physical</label>
          </div>
          <div class="col-12" style="text-align: right;">
            <button type="button" class="btn btn-primary" disabled={!canAdd} on:click={addDevice}>Add</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</div>