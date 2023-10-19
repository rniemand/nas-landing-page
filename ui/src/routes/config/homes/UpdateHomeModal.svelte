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

	export let onHomeUpdated: () => void = () => {};
	export let disabled: boolean = false;
	export const editHome = (_home: HomeDto) => {
		home = _home;
		open = true;
	};

	let open = false;
	let submitting: boolean = false;
	let isValid: boolean = false;
	let home: HomeDto = createBlankHome();

	const updateHome = async () => {
		submitting = true;
		const response = await new HomeClient().updateHome(home);
		if (response.success) {
			toastSuccess('Home Updated', `Home "${home.homeName}" has been updated`);
			open = false;
			onHomeUpdated();
		} else {
			toastError('Error', response?.error || 'Failed to update home');
		}
		submitting = false;
	};

	const toggle = () => (open = !open);

	$: isValid = validateAddHome(home);
</script>

<Modal isOpen={open} {toggle}>
	<ModalHeader>Update Home</ModalHeader>
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
		<Button color="primary" disabled={!isValid || submitting || disabled} on:click={updateHome}>
			Update Home
		</Button>
	</ModalFooter>
</Modal>
