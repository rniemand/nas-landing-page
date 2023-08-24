<script lang="ts">
  import { Modal as BSModal } from 'bootstrap';
  import { onMount } from "svelte";
	import { ContainerClient, ContainerDto } from '../../nlp-api';
	import { ContainerHelper } from './ContainerHelper';
	import Spinner from '../Spinner.svelte';

  export let onContainerModified: () => void = () => {};
  export const show = (_container: ContainerDto) => {
    container = _container;
    _modal.show();
  };

  let _modal: BSModal;
  let _modalVisible: boolean = false;
  let canSave: boolean = false;
  let saving: boolean = false;
  let message: string = '';
  let container: ContainerDto;

  const onModalShown = () => _modalVisible = true;
  const onModalHidden = () => _modalVisible = false;

  const syncRequest = () => {
    canSave = false;
    container.containerLabel = ContainerHelper.generateLabel(container);
    if(container.containerName.length < 2) return;
    canSave = true;
  };

  const saveChanges = async () => {
    canSave = false;
    saving = true;
    const response = await new ContainerClient().updateContainer(container);
    saving = false;

    if(!response.success) {
      message = response.error || '';
      return;
    }

    onContainerModified();
    _modal.hide();
  };

  onMount(() => {
    _modal = BSModal.getOrCreateInstance('#editContainerModal');
    document.getElementById('editContainerModal')?.addEventListener('show.bs.modal', onModalShown);
    document.getElementById('editContainerModal')?.addEventListener('hidden.bs.modal', onModalHidden);
    return () => {
      document.getElementById('editContainerModal')?.removeEventListener('show.bs.modal', onModalShown);
      document.getElementById('editContainerModal')?.removeEventListener('hidden.bs.modal', onModalHidden);
      _modal.dispose();
    };
  });
</script>

<div class="modal fade" id="editContainerModal" tabindex="-1" aria-labelledby="editContainerModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="editContainerModalLabel">Edit Container</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if saving}
          <Spinner />
          <div class="text-center mt-2">Saving changes...</div>
        {:else}
          {#if container}
            <form class="row g-3">
              <div class="col-md-3">
                <label for="shelf" class="form-label">Shelf</label>
                <input type="number" class="form-control" id="shelf" min="1" max="10" bind:value={container.shelfNumber} disabled>
              </div>
              <div class="col-md-3">
                <label for="level" class="form-label">Level</label>
                <input type="number" class="form-control" id="level" min="1" max="26" bind:value={container.shelfLevel} disabled>
              </div>
              <div class="col-md-3">
                <label for="row" class="form-label">Row</label>
                <input type="number" class="form-control" id="row" min="1" max="5" bind:value={container.shelfRow} disabled>
              </div>
              <div class="col-md-3">
                <label for="position" class="form-label">Position</label>
                <input type="number" class="form-control" id="position" min="1" max="5" bind:value={container.shelfRowPosition} disabled>
              </div>
              <div class="col-md-4">
                <label for="containerLabel" class="form-label">Label</label>
                <input type="text" class="form-control" id="containerLabel" disabled  bind:value={container.containerLabel}>
              </div>
              <div class="col-md-8">
                <label for="containerName" class="form-label">Name</label>
                <input type="text" class="form-control" id="containerName" bind:value={container.containerName} on:keyup={syncRequest} on:change={syncRequest}>
              </div>
              <div class="mb-3">
                <label for="containerNotes" class="form-label">Notes</label>
                <textarea class="form-control" id="containerNotes" rows="3" bind:value={container.notes} on:keyup={syncRequest} on:change={syncRequest}></textarea>
              </div>
              {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
              <div class="col-12" style="text-align: right;">
                <button type="button" class="btn btn-primary" disabled={!canSave} on:click={saveChanges}>Save Changes</button>
              </div>
            </form>
          {/if}
        {/if}
      </div>
    </div>
  </div>
</div>
