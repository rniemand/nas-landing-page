<script lang="ts">
  import { Modal as BSModal } from 'bootstrap';
  import { onMount } from 'svelte';
  import { ClassifyNetworkDeviceRequest, NetworkClient, type NetworkDeviceDto } from '../../nlp-api';

  let myModal: BSModal;
  let canAdd: boolean = false;
  let modalVisible: boolean = false;
  let deviceName: string = '';
  let info: string = '';
  let error: string = '';
  let device: NetworkDeviceDto | undefined = undefined;
  let request: ClassifyNetworkDeviceRequest = new ClassifyNetworkDeviceRequest();

  const onModalShown = () => modalVisible = true;
  const onModalHidden = () => modalVisible = false;

  export const open = (_device: NetworkDeviceDto) => {
    device = _device;
    resetRequest(_device.deviceID);
    deviceName = _device.deviceName;
    myModal.show();
  };

  const resetRequest = (deviceID: number) => {
    request = new ClassifyNetworkDeviceRequest({
      category: '',
      deviceID: deviceID
    });
  };

  const validate = () => {
    canAdd = false;
    if(request.category.length < 2) return;
    canAdd = true;
  };

  const classifyDevice = async () => {
    const response = await new NetworkClient().classifyDevice(request);
    if(response.success) {
      resetRequest(0);
      info = 'Device classified successfully';
      setTimeout(() => { info = ''; }, 2000);
    } else {
      error = response.error || '';
      setTimeout(() => { error = ''; }, 2000);
    }
  };

  onMount(() => {
    myModal = BSModal.getOrCreateInstance('#addClassification');
    document.getElementById('addClassification')?.addEventListener('show.bs.modal', onModalShown);
    document.getElementById('addClassification')?.addEventListener('hidden.bs.modal', onModalHidden);
    return () => {
      document.getElementById('addClassification')?.removeEventListener('show.bs.modal', onModalShown);
      document.getElementById('addClassification')?.removeEventListener('hidden.bs.modal', onModalHidden);
      myModal.dispose();
    };
  });
</script>

<div class="modal fade" id="addClassification" tabindex="-1" aria-labelledby="addClassificationLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="addClassificationLabel">Classify: {deviceName}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if info}<div class="alert alert-success" role="alert">{info}</div>{/if}
        {#if error}<div class="alert alert-danger" role="alert">{error}</div>{/if}
        <form class="row g-3">
          <div class="col-md-6">
            <label for="name" class="form-label">Name</label>
            <input type="text" class="form-control" id="name" bind:value={request.category} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="floor" class="form-label">Floor</label>
            <input type="text" class="form-control" id="floor" bind:value={request.subCategory} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="room" class="form-label">Room</label>
            <input type="text" class="form-control" id="room" bind:value={request.manufacturer} on:keyup={validate}>
          </div>
          <div class="col-md-6">
            <label for="roomLocation" class="form-label">Room Location</label>
            <input type="text" class="form-control" id="roomLocation" bind:value={request.model} on:keyup={validate}>
          </div>
          <div class="col-12" style="text-align: right;">
            <button type="button" class="btn btn-primary" disabled={!canAdd} on:click={classifyDevice}>Classify</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</div>