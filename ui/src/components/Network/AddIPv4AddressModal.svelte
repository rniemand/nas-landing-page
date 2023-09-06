<script lang="ts">
  // import { Modal as BSModal } from 'bootstrap';
  import { onMount } from 'svelte';
	import { AddNetworkIPv4Request, NetworkClient, type NetworkDeviceDto } from '../../nlp-api';

  // let myModal: BSModal;
  let deviceName: string = '';
  let modalVisible: boolean = false;
  let device: NetworkDeviceDto | undefined = undefined;
  let request: AddNetworkIPv4Request = new AddNetworkIPv4Request();
  let info: string = '';
  let error: string = '';
  let canAdd: boolean = false;

  const onModalShown = () => modalVisible = true;
  const onModalHidden = () => modalVisible = false;

  const resetRequest = (deviceID: number) => {
    request = new AddNetworkIPv4Request({
      deviceID: deviceID,
      connection: '',
      iPv4: '',
    });
  };

  const validate = () => {
    canAdd = false;
    if(request.connection.length < 2) return;
    canAdd = true;
  };

  const addIPv4Address = async () => {
    const response = await new NetworkClient().addIPv4Address(request);
    if(response.success) {
      resetRequest(0);
      info = 'IPv4 Address successfully added';
      setTimeout(() => { info = ''; }, 2000);
    } else {
      error = response.error || '';
      setTimeout(() => { error = ''; }, 2000);
    }
  };

  export const open = (_device: NetworkDeviceDto) => {
    device = _device;
    resetRequest(_device.deviceID);
    deviceName = _device.deviceName;
    // myModal.show();
  };

  onMount(() => {
    // myModal = BSModal.getOrCreateInstance('#addIPv4Address');
    // document.getElementById('addIPv4Address')?.addEventListener('show.bs.modal', onModalShown);
    // document.getElementById('addIPv4Address')?.addEventListener('hidden.bs.modal', onModalHidden);
    return () => {
      // document.getElementById('addIPv4Address')?.removeEventListener('show.bs.modal', onModalShown);
      // document.getElementById('addIPv4Address')?.removeEventListener('hidden.bs.modal', onModalHidden);
      // myModal.dispose();
    };
  });
</script>

<div class="modal fade" id="addIPv4Address" tabindex="-1" aria-labelledby="addIPv4AddressLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="addIPv4AddressLabel">Add IPv4 Address: {deviceName}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if info}<div class="alert alert-success" role="alert">{info}</div>{/if}
        {#if error}<div class="alert alert-danger" role="alert">{error}</div>{/if}
        <form class="row g-3">
          <div class="col-md-6">
            <label for="mac" class="form-label">MAC Address</label>
            <input type="text" class="form-control" id="mac" bind:value={request.macAddress} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="ipv4address" class="form-label">IPv4 Address</label>
            <input type="text" class="form-control" id="ipv4address" bind:value={request.iPv4} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="connection" class="form-label">Connection</label>
            <select class="form-select" bind:value={request.connection} on:change={validate}>
              <option value="LAN">LAN</option>
              <option value="WiFi">WiFi</option>
            </select>
          </div>
          <div class="col-md-6">
            <label for="networkName" class="form-label">Network Name</label>
            <input type="text" class="form-control" id="networkName" bind:value={request.networkName} on:keyup={validate}>
          </div>
          <div class="col-12" style="text-align: right;">
            <button type="button" class="btn btn-primary" disabled={!canAdd} on:click={addIPv4Address}>Add IPv4 Address</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</div>