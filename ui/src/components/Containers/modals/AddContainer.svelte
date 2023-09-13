<style>
  .add-btn {
    position: fixed;
    bottom: 10px;
    right: 10px;
    z-index: 20;
  }
</style>

<script lang="ts">
	import { ContainerClient, ContainerDto } from '../../../nlp-api';
	import { ContainerHelper } from '../ContainerHelper';
	import Spinner from '../../Spinner.svelte';

  export let onContainerAdded: () => void = () => {};
  let modalDialog: HTMLDialogElement;
  let request: ContainerDto = new ContainerDto();
  let canAdd: boolean = false;
  let message: string = '';
  let saving: boolean = false;

  const showModal = () => {
    resetRequest();
    modalDialog.showModal();
  };

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
    modalDialog.close();
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
</script>

<button class="btn btn-success btn-circle add-btn text-xl text-green-200" on:click={showModal}>+</button>

<dialog class="modal modal-bottom sm:modal-middle" bind:this={modalDialog}>
  <div class="modal-box">
    <h2 class="font-bold text-lg">Add Container</h2>
      {#if saving}
        <Spinner />
        <div class="text-center mt-2">Adding container</div>
      {:else}
        <form>
          <div class="flex">
            <div class="form-control flex-1 mr-2">
              <label class="label" for="shelf">
                <span class="label-text">Shelf</span>
              </label>
              <input type="number" class="input input-bordered w-full" id="shelf" min="1" max="10" bind:value={request.shelfNumber} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="form-control flex-1 mr-2">
              <label class="label" for="level">
                <span class="label-text">Level</span>
              </label>
              <input type="number" class="input input-bordered w-full" id="level" min="1" max="26" bind:value={request.shelfLevel} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="form-control flex-1 mr-2">
              <label class="label" for="row">
                <span class="label-text">Row</span>
              </label>
              <input type="number" class="input input-bordered w-full" id="row" min="1" max="5" bind:value={request.shelfRow} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="form-control flex-1">
              <label class="label" for="position">
                <span class="label-text">Position</span>
              </label>
              <input type="number" class="input input-bordered w-full" id="position" min="1" max="5" bind:value={request.shelfRowPosition} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
          </div>
          <div class="form-control w-full">
            <label class="label" for="containerLabel">
              <span class="label-text">Label</span>
            </label>
            <input type="text" class="input input-bordered w-full" id="containerLabel" disabled  bind:value={request.containerLabel}>
          </div>
          <div class="form-control w-full">
            <label class="label" for="containerName">
              <span class="label-text">Name</span>
            </label>
            <input type="text" class="input input-bordered w-full" id="containerName" bind:value={request.containerName} on:keyup={syncRequest} on:change={syncRequest}>
          </div>
          {#if message.length > 0}
            <div class="alert alert-warning my-2" role="alert">{message}</div>
          {/if}
          <button type="button" class="btn btn-primary w-full mt-2" disabled={!canAdd} on:click={addContainer}>Add Container</button>
        </form>
      {/if}
  </div>
  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>