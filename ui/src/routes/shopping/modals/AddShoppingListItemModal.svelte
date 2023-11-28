<script lang="ts">
	import {
		Button,
		Modal,
		ModalHeader,
		ModalBody,
		Form,
		ModalFooter,
		FormGroup,
		Col,
		Row,
		Label,
		Input
	} from 'sveltestrap';
	import StoreNameAutoComplete from '../components/StoreNameAutoComplete.svelte';
	import CategoryAutoComplete from '../components/CategoryAutoComplete.svelte';
	import ShoppingListItemAutoComplete from '../components/ShoppingListItemAutoComplete.svelte';
	import {
		ShoppingListClient,
		type ShoppingListItemDto,
		type WhoAmIResponse
	} from '../../../nlp-api';
	import { createNewShoppingListItem, validateAddShoppingListItem } from '../shopping';
	import { getContext } from 'svelte';
	import { toastError, toastSuccess } from '../../../components/ToastManager';
	import type { Writable } from 'svelte/store';
	import { AppContext } from '../../../enums/AppContext';

	const user = getContext<Writable<WhoAmIResponse | undefined>>(AppContext.User);
	let open = false;
	let canAdd: boolean = false;
	let item: ShoppingListItemDto = createNewShoppingListItem(0);
	export let onItemAdded: () => void;

	const toggle = () => {
		open = !open;
		if (open) item = createNewShoppingListItem($user?.homeId || 0);
	};

	const addShoppingListItem = async () => {
		item.addedByUserId = $user?.userId || 0;
		const response = await new ShoppingListClient().addShoppingListItem(item);

		if (!response.success) {
			toastError('Error Adding Item', response.error || 'Something went wrong!');
			return;
		}

		toastSuccess('Item Added', `Added '${item.itemName}' to your shopping list`);
		item = createNewShoppingListItem($user?.homeId || 0);
		onItemAdded();
		toggle();
	};

	const getLastKnownPrice = async (_item: ShoppingListItemDto) => {
		if ((_item.lastKnownPrice || 0) > 0) return;
		const response = await new ShoppingListClient().getLastKnownPrice(_item);
		item.lastKnownPrice = response;
		item = item;
	};

	$: canAdd = validateAddShoppingListItem(item);
	$: canAdd && getLastKnownPrice(item);
</script>

<div>
	<Button color="success" on:click={toggle}>Add Item</Button>
	<Modal isOpen={open} {toggle}>
		<ModalHeader {toggle}>Add Shopping List Item</ModalHeader>
		<ModalBody>
			<Form>
				<Row>
					<Col xs="6">
						<FormGroup>
							<Label>Store</Label>
							<StoreNameAutoComplete bind:value={item.storeName} placeholder="Store Name" />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Category</Label>
							<CategoryAutoComplete bind:value={item.category} placeholder="Item Category" />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<Col xs="9">
						<FormGroup>
							<Label>Item</Label>
							<ShoppingListItemAutoComplete bind:value={item.itemName} placeholder="Item Name" />
						</FormGroup>
					</Col>
					<Col>
						<FormGroup>
							<Label>Qty.</Label>
							<Input type="number" min={1} bind:value={item.quantity} />
						</FormGroup>
					</Col>
				</Row>
				<Row>
					<FormGroup>
						<Label>Last Known Price</Label>
						<Input type="number" bind:value={item.lastKnownPrice} />
					</FormGroup>
				</Row>
			</Form>
		</ModalBody>
		<ModalFooter>
			<Button color="primary" disabled={!canAdd} on:click={addShoppingListItem}>Add Item</Button>
		</ModalFooter>
	</Modal>
</div>
