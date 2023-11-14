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

	let items: ShoppingListItemDto[] = [];
	let loading: boolean = false;
	let editModal: EditShoppingListItemModal;
	let store: string = '';
	let category: string = '';
	let storeList: StoreNameDropdown;
	let categoryList: CategoryDropdown;

	const refreshShoppingList = async (_store: string, _category: string) => {
		loading = true;
		items = await new ShoppingListClient().getShoppingList(
			new BasicSearchRequest({
				filter: _store,
				subFilter: _category
			})
		);
		loading = false;
	};

	const onEditItem = (item: ShoppingListItemDto) => {
		editModal.edit(item);
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
		<div class="text-end">
			<AddShoppingListItemModal onItemAdded={handleChange} />
			<EditShoppingListItemModal bind:this={editModal} onEdited={handleChange} />
		</div>

		<div class="my-3 d-flex">
			<StoreNameDropdown
				clearButton
				bind:this={storeList}
				allowAllOption
				bind:value={store}
				className="me-2" />
			<CategoryDropdown clearButton bind:this={categoryList} allowAllOption bind:value={category} />
		</div>

		{#if !loading && items.length > 0}
			<div class="mt-2">
				<Accordion>
					{#each items as item}
						<AccordionItem>
							<span class="m-0" slot="header">
								({item.quantity}x) {item.itemName}
							</span>
							<ShoppingListItemInfo {item} {onEditItem} />
						</AccordionItem>
					{/each}
				</Accordion>
			</div>
		{:else}
			<div>You have no shopping list items</div>
		{/if}
	</Col>
</Row>
