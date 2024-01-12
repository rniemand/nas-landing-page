<script lang="ts">
	import { Row, Col, Accordion, AccordionItem } from 'sveltestrap';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import { AppUrls } from '../../enums/AppUrls';
	import AddShoppingListItemModal from './modals/AddShoppingListItemModal.svelte';
	import { BasicSearchRequest, ShoppingListClient, ShoppingListItemDto } from '../../nlp-api';
	import ShoppingListItemInfo from './components/ShoppingListItemInfo.svelte';
	import EditShoppingListItemModal from './modals/EditShoppingListItemModal.svelte';
	import StoreNameDropdown from './components/StoreNameDropdown.svelte';
	import CategoryDropdown from './components/CategoryDropdown.svelte';
	import ItemBoughtModal from './modals/ItemBoughtModal.svelte';
	import { toastError, toastSuccess } from '../../components/ToastManager';

	let items: ShoppingListItemDto[] = [];
	let loading: boolean = false;
	let editModal: EditShoppingListItemModal;
	let boughtModal: ItemBoughtModal;
	let store: string = '';
	let category: string = '';
	let storeList: StoreNameDropdown;
	let categoryList: CategoryDropdown;
	let listTotal: number = 0;

	const refreshShoppingList = async (_store: string, _category: string) => {
		loading = true;
		items = await new ShoppingListClient().getShoppingList(
			new BasicSearchRequest({
				filter: _store,
				subFilter: _category,
				includeCompletedEntries: false
			})
		);
		for (const item of items) {
			listTotal += (item.lastKnownPrice || 0) * item.quantity;
		}

		listTotal = Math.round(listTotal * 100) / 100;
		loading = false;
	};

	const onEditItem = (item: ShoppingListItemDto) => {
		editModal.edit(item);
	};

	const onItemBought = (item: ShoppingListItemDto) => {
		boughtModal.markBought(item);
	};

	const onDeleteItem = async (item: ShoppingListItemDto) => {
		const response = await new ShoppingListClient().deleteItem(item);
		if (!response.success) {
			toastError('Remove Failed', response.error || 'Failed to remove item');
			return;
		}
		toastSuccess('Item Removed', `Removed '${item.itemName}' from list`);
		refreshShoppingList(store, category);
	};

	const handleChange = () => {
		storeList.refreshStoreNames();
		categoryList.refreshCategories();
		refreshShoppingList(store, category);
	};

	$: refreshShoppingList(store, category);
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Shopping" />
</NavigationCrumbs>

<Row>
	<Col>
		<div class="my-3 d-flex">
			<StoreNameDropdown bind:this={storeList} allowAllOption bind:value={store} className="me-2" />
			<CategoryDropdown
				bind:this={categoryList}
				allowAllOption
				bind:value={category}
				className="me-2" />
			<AddShoppingListItemModal onItemAdded={handleChange} />
			<EditShoppingListItemModal bind:this={editModal} onEdited={handleChange} />
			<ItemBoughtModal bind:this={boughtModal} onItemBought={handleChange} />
		</div>

		{#if !loading && items.length > 0}
			{#if listTotal > 0}
				<h4 class="text-center mb-3">Total: $ {listTotal}</h4>
			{/if}
			<div class="mt-2">
				<Accordion>
					{#each items as item}
						<AccordionItem>
							<span class="m-0" slot="header">
								({item.quantity}x) {item.itemName}
								{#if (item.lastKnownPrice || 0) > 0}
									- ${item.lastKnownPrice}
								{/if}
							</span>
							<ShoppingListItemInfo {item} {onEditItem} {onItemBought} {onDeleteItem} />
						</AccordionItem>
					{/each}
				</Accordion>
			</div>
		{:else}
			<div>You have no shopping list items</div>
		{/if}
	</Col>
</Row>
