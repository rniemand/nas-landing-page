<script lang="ts">
	import {
		Button,
		Col,
		Form,
		FormGroup,
		Input,
		Label,
		Modal,
		ModalBody,
		ModalFooter,
		ModalHeader,
		Row
	} from 'sveltestrap';
	import { HomeClient, type HomeDto } from '../../../nlp-api';
	import { createBlankHome, validateAddHome } from './homes';
	import { toastError, toastSuccess } from '../../../components/ToastManager';

	export let onHomeAdded: () => void = () => {};
	export let disabled: boolean = false;
	let open = false;
	let submitting: boolean = false;
	let isValid: boolean = false;
	let home: HomeDto = createBlankHome();

	const addHome = async () => {
		submitting = true;
		const response = await new HomeClient().addHome(home);
		if (response.success) {
			toastSuccess('Home Added', `Home "${home.homeName}" has been added`);
			open = false;
			onHomeAdded();
		} else {
			toastError('Error', response?.error || 'Failed to add home');
		}
		submitting = false;
	};

	const toggle = () => {
		open = !open;
		if (open) home = createBlankHome();
	};

	$: isValid = validateAddHome(home);
</script>

<Button color="success" on:click={toggle}>Add Home</Button>
<Modal isOpen={open} {toggle}>
	<ModalHeader>Add Home</ModalHeader>
	<ModalBody>
		<Form>
			<Row>
				<FormGroup>
					<Label>Home Name</Label>
					<Input type="text" placeholder="Home Name" bind:value={home.homeName} />
				</FormGroup>
			</Row>
			<Row>
				<Col>
					<FormGroup>
						<Label>Latitude</Label>
						<Input type="number" placeholder="Latitude" bind:value={home.latitude} />
					</FormGroup>
				</Col>
				<Col>
					<FormGroup>
						<Label>Longitude</Label>
						<Input type="number" placeholder="Longitude" bind:value={home.longitude} />
					</FormGroup>
				</Col>
			</Row>
			<Row>
				<FormGroup>
					<Label>Address Line 1</Label>
					<Input type="text" placeholder="Address Line 1" bind:value={home.addressLine1} />
				</FormGroup>
			</Row>
			<Row>
				<FormGroup>
					<Label>Address Line 2</Label>
					<Input type="text" placeholder="Address Line 2" bind:value={home.addressLine2} />
				</FormGroup>
			</Row>
			<Row>
				<Col>
					<FormGroup>
						<Label>City</Label>
						<Input type="text" placeholder="City" bind:value={home.city} />
					</FormGroup>
				</Col>
				<Col>
					<FormGroup>
						<Label>Postal Code</Label>
						<Input type="text" placeholder="Postal Code" bind:value={home.postalCode} />
					</FormGroup>
				</Col>
			</Row>
			<Row>
				<Col>
					<FormGroup>
						<Label>Country</Label>
						<Input type="text" placeholder="Country" bind:value={home.country} />
					</FormGroup>
				</Col>
				<Col>
					<FormGroup>
						<Label>Province</Label>
						<Input type="text" placeholder="Province" bind:value={home.province} />
					</FormGroup>
				</Col>
			</Row>
		</Form>
	</ModalBody>
	<ModalFooter>
		<Button color="primary" disabled={!isValid || submitting || disabled} on:click={addHome}>
			Add Home
		</Button>
	</ModalFooter>
</Modal>
