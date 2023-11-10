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
	import StoreNameAutoComplete from './StoreNameAutoComplete.svelte';
	import CategoryAutoComplete from './CategoryAutoComplete.svelte';
	import ShoppingListItemAutoComplete from './ShoppingListItemAutoComplete.svelte';
	import {
		ShoppingListClient,
		type ShoppingListItemDto,
		type WhoAmIResponse
	} from '../../../nlp-api';
	import { createNewShoppingListItem, validateAddShoppingListItem } from '../shopping';
	import { authContext } from '../../../utils/AppStore';
	import { onMount } from 'svelte';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	let open = false;
	let canAdd: boolean = false;
	let userContext: WhoAmIResponse;
	let item: ShoppingListItemDto = createNewShoppingListItem(0);
	export let onItemAdded: () => void;

	const toggle = () => {
		open = !open;
		if (open) item = createNewShoppingListItem(userContext.homeId);
	};

	const addShoppingListItem = async () => {
		item.addedByUserId = userContext.userId;
		const response = await new ShoppingListClient().addShoppingListItem(item);

		if (!response.success) {
			toastError('Error Adding Item', response.error || 'Something went wrong!');
			return;
		}

		toastSuccess('Item Added', `Added '${item.itemName}' to your shopping list`);
		item = createNewShoppingListItem(userContext.homeId);
		onItemAdded();
		toggle();
	};

	onMount(() => {
		return authContext.subscribe((_whoAmI) => {
			if (!_whoAmI) return;
			userContext = _whoAmI;
		});
	});

	$: canAdd = validateAddShoppingListItem(item);
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
