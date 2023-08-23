<script lang="ts">
  import { Modal as BSModal } from 'bootstrap';
  import { onMount } from "svelte";
	import { ContainerClient, ContainerDto, ContainerItemDto } from '../../nlp-api';
	import { ContainerHelper } from './ContainerHelper';
	import Spinner from '../Spinner.svelte';
	import ContainerCategoryInput from './ContainerCategoryInput.svelte';
	import { error } from '@sveltejs/kit';

  export let container: ContainerDto | undefined = undefined;
  export let onItemAdded: () => void = () => {};

  let _modal: BSModal;
  let _modalVisible: boolean = false;
  let request: ContainerItemDto = new ContainerItemDto();
  let canAdd: boolean = false;
  let message: string = '';
  let saving: boolean = false;

  const onModalShown = () => _modalVisible = true;
  const onModalHidden = () => _modalVisible = false;

  const addItem = async () => {
    saving = true;
    const response = await new ContainerClient().addContainerItem(request);
    saving = false;

    if(response.success) {
      onItemAdded();
      resetRequest();
      _modal.hide();
      return;
    }
    
    message = response.error || '';
  };

  const syncRequest = () => {
    canAdd = false;
    if(request.category?.length < 2) return;
    if(request.subCategory?.length < 2) return;
    if(request.inventoryName?.length < 2) return;
    canAdd = true;
  };

  const resetRequest = () => {
    request = new ContainerItemDto({
      autoFlagOrderMore: false,
      category: '',
      containerId: container?.containerId || 0,
      dateAddedUtc: new Date(),
      dateUpdatedUtc: new Date(),
      inventoryName: '',
      itemId: 0,
      orderMore: false,
      orderMoreMinQty: 0,
      orderPlaced: false,
      orderUrl: '',
      quantity: 0,
      subCategory: '',
    });
    syncRequest();
  };
  
  const updateModal = (_visible: boolean) => {
    if(!_visible) return;
    resetRequest();
  };

  onMount(() => {
    _modal = BSModal.getOrCreateInstance('#addContainerItemModal');
    document.getElementById('addContainerItemModal')?.addEventListener('show.bs.modal', onModalShown);
    document.getElementById('addContainerItemModal')?.addEventListener('hidden.bs.modal', onModalHidden);
    return () => {
      document.getElementById('addContainerItemModal')?.removeEventListener('show.bs.modal', onModalShown);
      document.getElementById('addContainerItemModal')?.removeEventListener('hidden.bs.modal', onModalHidden);
      _modal.dispose();
    };
  });

  $: updateModal(_modalVisible);
</script>

{#if container}
  <button class="btn btn-success" on:click={() => _modal.show()}>Add Item</button>
{/if}

<div class="modal fade" id="addContainerItemModal" tabindex="-1" aria-labelledby="addContainerItemModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="addContainerItemModalLabel">Add Container Items ({container?.containerLabel})</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if saving}
          <Spinner />
          <div class="text-center mt-2">Adding item(s)...</div>
        {:else}
          <form class="row g-3">
            <div class="col-md-4">
              <label for="quantity" class="form-label">Quantity</label>
              <input type="number" class="form-control" min="0" id="quantity" bind:value={request.quantity} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-4">
              <label for="orderMoreQty" class="form-label">Order More Qty</label>
              <input type="number" class="form-control" min="0" id="orderMoreQty" bind:value={request.orderMoreMinQty} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-4">
              <label for="orderUrl" class="form-label">Order URL</label>
              <input type="text" class="form-control" id="orderUrl" bind:value={request.orderUrl} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            
            <div class="col-md-6">
              <label for="category" class="form-label">Category</label>
              <ContainerCategoryInput bind:value={request.category} onChange={syncRequest} />
            </div>
            <div class="col-md-6">
              <label for="subCategory" class="form-label">Sub subCategory</label>
              <input type="text" class="form-control" id="subCategory" bind:value={request.subCategory} on:keyup={syncRequest} on:change={syncRequest}>
            </div>

            <div class="col-md-12">
              <label for="itemName" class="form-label">Item Name</label>
              <input type="text" class="form-control" id="itemName" bind:value={request.inventoryName} on:keyup={syncRequest} on:change={syncRequest}>
            </div>

            <div class="d-flex">
              <div class="form-check form-switch form-check-inline">
                <input class="form-check-input" type="checkbox" role="switch" id="orderMore" bind:checked={request.orderMore} on:change={syncRequest}>
                <label class="form-check-label" for="orderMore">Order More</label>
              </div>
              <div class="form-check form-switch form-check-inline">
                <input class="form-check-input" type="checkbox" role="switch" id="orderPlaced" bind:checked={request.orderPlaced} on:change={syncRequest}>
                <label class="form-check-label" for="orderPlaced">Order Placed</label>
              </div>
              <div class="form-check form-switch form-check-inline">
                <input class="form-check-input" type="checkbox" role="switch" id="autoFlagOrderMore" bind:checked={request.autoFlagOrderMore} on:change={syncRequest}>
                <label class="form-check-label" for="autoFlagOrderMore">Auto Flag Order</label>
              </div>
            </div>

            {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
            <div class="col-12" style="text-align: right;">
              <button type="button" class="btn btn-primary" disabled={!canAdd} on:click={addItem}>Add Item(s)</button>
            </div>
          </form>
        {/if}
      </div>
    </div>
  </div>
</div>
