<script lang="ts">
	import { ContainerClient, ContainerDto } from '../../../nlp-api';
	import { ContainerHelper } from '../ContainerHelper';
	import Spinner from '../../Spinner.svelte';

  export let onContainerModified: () => void = () => {};
  export const show = (_container: ContainerDto) => {
    container = _container;
    modalDialog.show();
  };

  let canSave: boolean = false;
  let saving: boolean = false;
  let message: string = '';
  let container: ContainerDto;
  let modalDialog: HTMLDialogElement;

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
    modalDialog.close();
  };
</script>

<dialog class="modal modal-bottom sm:modal-middle" bind:this={modalDialog}>
  <div class="modal-box">
    <h2 class="font-bold text-lg">Edit Container</h2>
    {#if saving}
    <Spinner />
    <div class="text-center mt-2">Saving changes...</div>
  {:else}
    {#if container}
      <form>
        <div class="flex">
          <div class="form-control flex-1 mr-2">
            <label class="label" for="shelf">
              <span class="label-text">Shelf</span>
            </label>
            <input type="number" class="input input-bordered w-full" id="shelf" min="1" max="10" bind:value={container.shelfNumber} disabled>
          </div>
          <div class="form-control flex-1 mr-2">
            <label class="label" for="level">
              <span class="label-text">Level</span>
            </label>
            <input type="number" class="input input-bordered w-full" id="level" min="1" max="26" bind:value={container.shelfLevel} disabled>
          </div>
          <div class="form-control flex-1 mr-2">
            <label class="label" for="row">
              <span class="label-text">Row</span>
            </label>
            <input type="number" class="input input-bordered w-full" id="row" min="1" max="5" bind:value={container.shelfRow} disabled>
          </div>
          <div class="form-control flex-1">
            <label class="label" for="position">
              <span class="label-text">Position</span>
            </label>
            <input type="number" class="input input-bordered w-full" id="position" min="1" max="5" bind:value={container.shelfRowPosition} disabled>
          </div>
        </div>
        <div class="form-control">
          <label class="label" for="containerLabel">
            <span class="label-text">Label</span>
          </label>
          <input type="text" class="input input-bordered w-full" id="containerLabel" disabled  bind:value={container.containerLabel}>
        </div>
        <div class="form-control">
          <label class="label" for="containerName">
            <span class="label-text">Name</span>
          </label>
          <input type="text" class="input input-bordered w-full" id="containerName" bind:value={container.containerName} on:keyup={syncRequest} on:change={syncRequest}>
        </div>
        <div class="mb-3">
          <label class="label" for="containerNotes">
            <span class="label-text">Notes</span>
          </label>
          <textarea class="input input-bordered w-full" id="containerNotes" rows="3" bind:value={container.notes} on:keyup={syncRequest} on:change={syncRequest}></textarea>
        </div>
        {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
        <div class="col-12" style="text-align: right;">
          <button type="button" class="btn btn-primary w-full mt-2" disabled={!canSave} on:click={saveChanges}>Save Changes</button>
        </div>
      </form>
    {/if}
  {/if}
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>