<script lang="ts">
  import { Modal as BSModal } from 'bootstrap';
  import { onMount } from "svelte";
	import { ContainerClient, ContainerDto } from '../../nlp-api';
	import { ContainerHelper } from './ContainerHelper';
	import Spinner from '../Spinner.svelte';

  export let onContainerAdded: () => void = () => {};

  let _modal: BSModal;
  let _modalVisible: boolean = false;
  let request: ContainerDto = new ContainerDto();
  let canAdd: boolean = false;
  let message: string = '';
  let saving: boolean = false;

  const onModalShown = () => _modalVisible = true;
  const onModalHidden = () => _modalVisible = false;

  const addContainer = async () => {
    saving = true;
    const response = await new ContainerClient().addContainer(request);
    saving = false;

    if(!response.success) {
      message = response.error!;
      return;
    }

    onContainerAdded();
    resetRequest();
    _modal.hide();
  };

  const syncRequest = () => {
    canAdd = false;
    request.containerLabel = ContainerHelper.generateLabel(request);
    if(request.containerName.length < 2) return;
    checkIfContainerExists();
  };

  const resetRequest = () => {
    request = new ContainerDto({
      containerId: 0,
      containerLabel: '',
      containerName: '',
      dateAddedUtc: new Date(),
      dateUpdatedUtc: new Date(),
      itemCount: 0,
      notes: '',
      shelfLevel: 1,
      shelfNumber: 1,
      shelfRow: 1,
      shelfRowPosition: 1,
    });
    syncRequest();
  };

  const checkIfContainerExists = async () => {
    canAdd = false;
    const response = await new ContainerClient().checkContainerExists(request);
    canAdd = !response.success;
    message = canAdd ? '' : `A container already exists with the name: ${request.containerLabel}`;
  };

  const updateModal = (_visible: boolean) => {
    if(!_visible) return;
    resetRequest();
  };

  onMount(() => {
    _modal = BSModal.getOrCreateInstance('#addContainerModal');
    document.getElementById('addContainerModal')?.addEventListener('show.bs.modal', onModalShown);
    document.getElementById('addContainerModal')?.addEventListener('hidden.bs.modal', onModalHidden);
    return () => {
      document.getElementById('addContainerModal')?.removeEventListener('show.bs.modal', onModalShown);
      document.getElementById('addContainerModal')?.removeEventListener('hidden.bs.modal', onModalHidden);
      _modal.dispose();
    };
  });

  $: updateModal(_modalVisible);
</script>

<button class="btn btn-success" on:click={() => _modal.show()}>Add Container</button>

<div class="modal fade" id="addContainerModal" tabindex="-1" aria-labelledby="addContainerModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="addContainerModalLabel">Add Container</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if saving}
          <Spinner />
          <div class="text-center mt-2">Adding container</div>
        {:else}
          <form class="row g-3">
            <div class="col-md-3">
              <label for="shelf" class="form-label">Shelf</label>
              <input type="number" class="form-control" id="shelf" min="1" max="10" bind:value={request.shelfNumber} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-3">
              <label for="level" class="form-label">Level</label>
              <input type="number" class="form-control" id="level" min="1" max="26" bind:value={request.shelfLevel} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-3">
              <label for="row" class="form-label">Row</label>
              <input type="number" class="form-control" id="row" min="1" max="5" bind:value={request.shelfRow} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-3">
              <label for="position" class="form-label">Position</label>
              <input type="number" class="form-control" id="position" min="1" max="5" bind:value={request.shelfRowPosition} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-4">
              <label for="containerLabel" class="form-label">Label</label>
              <input type="text" class="form-control" id="containerLabel" disabled  bind:value={request.containerLabel}>
            </div>
            <div class="col-md-8">
              <label for="containerName" class="form-label">Name</label>
              <input type="text" class="form-control" id="containerName" bind:value={request.containerName} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
            <div class="col-12" style="text-align: right;">
              <button type="button" class="btn btn-primary" disabled={!canAdd} on:click={addContainer}>Add Container</button>
            </div>
          </form>
        {/if}
      </div>
    </div>
  </div>
</div>
