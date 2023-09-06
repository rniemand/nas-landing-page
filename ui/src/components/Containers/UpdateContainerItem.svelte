<script lang="ts">
  // import { Modal as BSModal } from 'bootstrap';
  import { onMount } from "svelte";
	import { ContainerClient, ContainerItemDto } from '../../nlp-api';
	import Spinner from '../Spinner.svelte';
	import ItemCategoryInput from './ItemCategoryInput.svelte';
	import ItemSubCategoryInput from './ItemSubCategoryInput.svelte';

  export let onItemSaved: () => void = () => {};
  export let item: ContainerItemDto = new ContainerItemDto();
  export const show = (_item: ContainerItemDto) => {
    item = _item;
    // _modal.show();
  };

  // let _modal: BSModal;
  let _modalVisible: boolean = false;
  let canAdd: boolean = false;
  let message: string = '';
  let saving: boolean = false;

  const onModalShown = () => _modalVisible = true;
  const onModalHidden = () => _modalVisible = false;

  const saveChanges = async () => {
    saving = true;
    const response = await new ContainerClient().updateContainerItem(item);
    saving = false;
    
    if(!response.success) {
      message = response.error || '';
      return;
    }

    onItemSaved();
    // _modal.hide()
  };

  const syncRequest = () => {
    canAdd = false;
    if(item.category?.length < 2) return;
    if(item.subCategory?.length < 2) return;
    if(item.inventoryName?.length < 2) return;
    canAdd = true;
  };
  
  onMount(() => {
    // _modal = BSModal.getOrCreateInstance('#updateContainerItemModal');
    // document.getElementById('updateContainerItemModal')?.addEventListener('show.bs.modal', onModalShown);
    // document.getElementById('updateContainerItemModal')?.addEventListener('hidden.bs.modal', onModalHidden);
    return () => {
      // document.getElementById('updateContainerItemModal')?.removeEventListener('show.bs.modal', onModalShown);
      // document.getElementById('updateContainerItemModal')?.removeEventListener('hidden.bs.modal', onModalHidden);
      // _modal.dispose();
    };
  });
</script>

<div class="modal fade" id="updateContainerItemModal" tabindex="-1" aria-labelledby="updateContainerItemModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="updateContainerItemModalLabel">Update Container Item</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if saving}
          <Spinner />
          <div class="text-center mt-2">Saving changes...</div>
        {:else}
          <form class="row g-3">
            <div class="col-md-4">
              <label for="quantity" class="form-label">Quantity</label>
              <input type="number" class="form-control" min="0" id="quantity" bind:value={item.quantity} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-4">
              <label for="orderMoreQty" class="form-label">Order More Qty</label>
              <input type="number" class="form-control" min="0" id="orderMoreQty" bind:value={item.orderMoreMinQty} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            <div class="col-md-4">
              <label for="orderUrl" class="form-label">Order URL</label>
              <input type="text" class="form-control" id="orderUrl" bind:value={item.orderUrl} on:keyup={syncRequest} on:change={syncRequest}>
            </div>
            
            <div class="col-md-6">
              <label for="category" class="form-label">Category</label>
              <ItemCategoryInput bind:value={item.category} onChange={syncRequest} />
            </div>
            <div class="col-md-6">
              <label for="subCategory" class="form-label">Sub subCategory</label>
              <ItemSubCategoryInput bind:value={item.subCategory} bind:category={item.category} onChange={syncRequest} />
            </div>

            <div class="col-md-12">
              <label for="itemName" class="form-label">Item Name</label>
              <input type="text" class="form-control" id="itemName" bind:value={item.inventoryName} on:keyup={syncRequest} on:change={syncRequest}>
            </div>

            <div class="d-flex">
              <div class="form-check form-switch form-check-inline">
                <input class="form-check-input" type="checkbox" role="switch" id="orderMore" bind:checked={item.orderMore} on:change={syncRequest}>
                <label class="form-check-label" for="orderMore">Order More</label>
              </div>
              <div class="form-check form-switch form-check-inline">
                <input class="form-check-input" type="checkbox" role="switch" id="orderPlaced" bind:checked={item.orderPlaced} on:change={syncRequest}>
                <label class="form-check-label" for="orderPlaced">Order Placed</label>
              </div>
              <div class="form-check form-switch form-check-inline">
                <input class="form-check-input" type="checkbox" role="switch" id="autoFlagOrderMore" bind:checked={item.autoFlagOrderMore} on:change={syncRequest}>
                <label class="form-check-label" for="autoFlagOrderMore">Auto Flag Order</label>
              </div>
            </div>

            {#if message.length > 0}<div class="alert alert-warning" role="alert">{message}</div>{/if}
            <div class="col-12" style="text-align: right;">
              <button type="button" class="btn btn-primary" disabled={!canAdd} on:click={saveChanges}>Save Changes</button>
            </div>
          </form>
        {/if}
      </div>
    </div>
  </div>
</div>
