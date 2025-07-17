namespace Oxpecker.Solid.Primitives

open System.Runtime.CompilerServices
open Fable.Core
open Fable.Core.JsInterop
open Fable.Core.JS
open Oxpecker.Solid
open Browser.Types

[<Erase>]
module Spec =
    let [<Literal>] primitives = "@solid-primitives/"
    let [<Literal>] utils = primitives + "utils"

type DisposeCallback = unit -> unit

[<Erase>]
module private ActiveElementSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/active-element"
        let [<Literal>] version = ""

open ActiveElementSpec

[<Erase; AutoOpen>]
type ActiveElement =
    /// <summary>
    /// Listen for changes to the <c>document.activeElement</c>
    /// </summary>
    /// <remarks>
    /// non reactive
    /// </remarks>
    [<ImportMember(Spec.path)>]
    static member makeActiveElementListener (handler: #HtmlElement -> unit): DisposeCallback = jsNative

    /// <summary>
    /// Attaches "blur" and "focus" event listeners to the element
    /// </summary>
    /// <param name="target"></param>
    /// <param name="callBack"></param>
    /// <param name="useCapture"></param>
    [<ImportMember(Spec.path)>]
    static member makeFocusListener (target: #HtmlElement, callBack: bool -> unit, ?useCapture: bool): DisposeCallback = jsNative

    /// <summary>
    /// Provides a reactive signal of <c>document.activeElement</c>. Check which element is currently focused.
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createActiveElement (): Accessor<#HtmlElement> = jsNative

    /// <summary>
    /// Provides a signal representing element's focus state
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createFocusSignal(target: #HtmlElement): Accessor<bool> = jsNative

    /// <summary>
    /// Provides a signal representing element's focus state
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createFocusSignal(target: Accessor<#HtmlElement>): Accessor<bool> = jsNative


[<Erase; AutoOpen>]
module private AudioSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/audio"

[<Global>]
type MediaSource = interface end
[<Erase>]
type AudioSource = U3<string, HTMLAudioElement, MediaSource>
[<RequireQualifiedAccess; StringEnum>]
type AudioState =
    | Loading
    | Playing
    | Paused
    | Complete
    | Stopped
    | Ready
    | Error
[<AllowNullLiteral; Interface>]
type AudioControls =
    /// Start playing
    abstract member play: unit -> unit
    /// Pause playing
    abstract member pause: unit -> unit
    /// Seeks to a location in the playhead
    abstract member seek: time: float -> unit
    /// Sets the volume of the player
    abstract member setVolume: volume: float -> unit


[<AllowNullLiteral; Interface>]
type AudioPlayer =
    inherit AudioControls
    /// Raw player instance
    abstract member player: HTMLAudioElement

[<AllowNullLiteral; Interface>]
type ReactiveAudioPlayer =
    abstract member player: HTMLAudioElement
    abstract member state: AudioState
    abstract member currentTime: float
    abstract member duration: float
    abstract member volume: float

[<Erase; AutoOpen>]
type Audio =
    /// <summary>
    /// A foundational primitive with no player controls but exposes the raw player
    /// object.
    /// </summary>
    /// <param name="src">Audio file path or MediaSource to be played</param>
    /// <param name="handlers">An array of handlers to bind against the player.</param>
    [<ImportMember(Spec.path)>]
    static member makeAudio(src: AudioSource, ?handlers: (*AudioEventHandlers*) obj): HTMLAudioElement = jsNative
    /// <summary>
    /// Provides a very basic interface for wrapping listeners to a supplied or default
    /// audio player.
    /// </summary>
    /// <remarks>
    /// The <c>seek</c> function falls back to <c>fastSeek</c> when on supporting browsers.
    /// </remarks>
    /// <param name="src"></param>
    /// <param name="handlers">Array of handlers to bind against the player</param>
    [<ImportMember(Spec.path)>]
    static member makeAudioPlayer(src: AudioSource, ?handlers: (*AudioEventHandlers*) obj): AudioPlayer = jsNative
    /// <summary>
    /// Creates a very basic audio/sound player with reactive properties to control
    /// the audio. Be careful not to destructure the value properties provided
    /// by the primitive as it will likely break reactivity.
    /// </summary>
    /// <remarks>
    /// The audio primitive exports reactive properties that provide you access
    /// to state, duration and current time.<br/><br/>
    /// Intialising the primitive with <c>playing</c> as true works, however
    /// note that the user has to interact with the page first (on a fresh
    /// page load).
    /// </remarks>
    /// <param name="src">Can be a reactive signal as well as a media source.</param>
    /// <param name="playing"></param>
    /// <param name="volume"></param>
    [<ImportMember(Spec.path)>]
    static member createAudio(
            src: AudioSource,
            ?playing: Accessor<bool>,
            ?volume: Accessor<float>
        ): ReactiveAudioPlayer * AudioControls = jsNative
    /// <summary>
    /// Creates a very basic audio/sound player with reactive properties to control
    /// the audio. Be careful not to destructure the value properties provided
    /// by the primitive as it will likely break reactivity.
    /// </summary>
    /// <remarks>
    /// The audio primitive exports reactive properties that provide you access
    /// to state, duration and current time.<br/><br/>
    /// Intialising the primitive with <c>playing</c> as true works, however
    /// note that the user has to interact with the page first (on a fresh
    /// page load).
    /// </remarks>
    /// <param name="src">Can be a reactive signal as well as a media source.</param>
    /// <param name="playing"></param>
    /// <param name="volume"></param>
    [<ImportMember(Spec.path)>]
    static member createAudio(
            src: Accessor<AudioSource>,
            ?playing: Accessor<bool>,
            ?volume: Accessor<float>
        ): ReactiveAudioPlayer * AudioControls = jsNative


[<Erase; AutoOpen>]
module private AutoFocusSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/autofocus"

[<Erase; AutoOpen>]
type AutoFocus =
    /// <summary>
    /// The <c>autofocus</c> directive uses the native <c>autofocus</c> attribute to determine it should focus
    /// the element or not. Using this directive without <c>autofocus={true}</c> (or the shorthand, <c>autofocus</c>)
    /// will not perform anything.
    /// </summary>
    /// <example>
    /// <code>
    /// // As a directive
    /// button(autofocus = true).use(autofocus)
    /// // As a ref
    /// button(autofocus = true).ref(autofocus)
    /// </code>
    /// </example>
    [<ImportMember(Spec.path)>]
    static member autofocus = jsNative

    /// <summary>
    /// Reactively autofocuses an element passid in as a signal
    /// </summary>
    /// <example><code>
    /// import { createAutofocus } from "@solid-primitives/autofocus";
    /// // Using ref
    /// let ref!: HTMLButtonElement;
    /// createAutofocus(() => ref);
    ///
    /// /button ref={ref}>Autofocused /button>;
    ///
    /// // Using ref signal
    /// const [ref, setRef] = createSignal /HTMLButtonElement>();
    /// createAutofocus(ref);
    ///
    /// /button ref={setRef}>Autofocused /button>;
    /// </code></example>
    [<ImportMember(Spec.path)>]
    static member createAutofocus (ref: unit -> #HtmlElement): unit = jsNative


[<Erase; AutoOpen>]
module BoundsSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/bounds"
        let [<Literal>] version = "0.1.0"

[<Interface; AllowNullLiteral>]
type ElementBounds =
    abstract width: int with get
    abstract height: int with get
    abstract top: int with get
    abstract left: int with get
    abstract right: int with get
    abstract bottom: int with get
[<Erase; AutoOpen>]
type Bounds =
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    /// <param name="trackScroll">Listen to window scroll events</param>
    /// <param name="trackMutation">Listen to changes to the dom structure/styles</param>
    /// <param name="trackResize">Listen to changes to the element's resize events</param>
    /// <remarks>All options are 'truthy' by default</remarks>
    [<ImportMember(Spec.path); ParamObject(1)>]
    static member createElementBounds(target: #HTMLElement,
                                      ?trackScroll: bool,
                                      ?trackMutation: bool,
                                      ?trackResize: bool): ElementBounds = jsNative
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    [<ImportMember(Spec.path)>]
    static member createElementBounds(target: #HTMLElement): ElementBounds = jsNative
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    /// <param name="trackScroll">Listen to window scroll events</param>
    /// <param name="trackMutation">Listen to changes to the dom structure/styles</param>
    /// <param name="trackResize">Listen to changes to the element's resize events</param>
    /// <remarks>All options are 'truthy' by default</remarks>
    [<ImportMember(Spec.path); ParamObject(1)>]
    static member createElementBounds(target: Accessor<#HTMLElement>,
                                      ?trackScroll: bool,
                                      ?trackMutation: bool,
                                      ?trackResize: bool): ElementBounds = jsNative
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    [<ImportMember(Spec.path)>]
    static member createElementBounds(target: Accessor<#HTMLElement>): ElementBounds = jsNative

[<Fable.Core.Erase>]
module BroadcastChannelSpec =
    [<Fable.Core.Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/broadcast-channel"
        let [<Literal>] version = "0.1.0"
open BroadcastChannelSpec

[<AllowNullLiteral; Interface>]
type MessageEvent<'T> =
    inherit MessageEvent
    abstract member data: 'T with get


[<AllowNullLiteral; Interface>]
type BroadcastChannelResult = interface end


[<AllowNullLiteral; Interface>]
type MakeBroadcastChannelResult<'T> =
    inherit BroadcastChannelResult
    /// A function to subscribe to messages from other tabs on the same channel
    abstract member onMessage: event: MessageEvent<'T> -> unit with get
    /// A function to send messages to other tabs
    abstract member postMessage: 'T -> unit with get
    /// A function to close the channel
    abstract member close: unit -> unit with get
    /// The name of the channel
    abstract member channelName: string with get
    /// The underlying BroadcastChannel instance
    abstract member instance: BroadcastChannel<'T> with get

and [<AllowNullLiteral; Interface>] CreateBroadcastChannelResult<'T> =
    inherit BroadcastChannelResult
    /// An accessor that updates when postMessage is fired from other contexts
    abstract member message: Accessor<'T> -> unit with get
    /// A function to send messages to other tabs
    abstract member postMessage: 'T -> unit with get
    /// A function to close the channel
    abstract member close: unit -> unit with get
    /// The name of the channel
    abstract member channelName: string with get
    /// The underlying BroadcastChannel instance
    abstract member instance: BroadcastChannel<'T> with get


and [<Erase>] BroadcastChannel<'T> =
    /// <summary>
    /// Creates a new BroadcastChannel instance for cross-tab communication.<br/>
    /// The channel name is used to identify the channel. If a channel with the same name already exists, it will
    /// be returned instead of creating a new one.<br/>
    /// Channel attempt closing the channel when the on owner cleanup. If there are multiple connected instances, the
    /// channel will not be closed until the last owner is removed.
    /// </summary>
    /// <param name="name">Channel name to listen/broadcast on</param>
    /// <returns>An object with the following properties<br/>
    /// onMessage, postMessage, close, channelName, instance</returns>
    [<ImportMember("@solid-primitives/broadcast-channel")>]
    static member makeBroadcastChannel<'T> (name: string): MakeBroadcastChannelResult<'T> = jsNative
    /// <summary>
    /// Provides the same functionality as <c>makeBroadcastChannel</c> but instead of returning <c>onMessage</c>, it
    /// returns a <c>message</c> signal accessor that updates when postMessage is fired from other contexts.
    /// </summary>
    /// <param name="name">Channel name to listen/broadcast on</param>
    /// <returns>An object with the following properties<br/>
    /// message, postMessage, close, channelName, instance</returns>
    [<ImportMember("@solid-primitives/broadcast-channel")>]
    static member createBroadcastChannel<'T> (name: string): CreateBroadcastChannelResult<'T> = jsNative


[<AutoOpen; Erase>]
module private ClipboardSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/clipboard"
        let [<Literal>] version = ""

[<StringEnum>]
type PresentationStyle =
    | Attachment
    | Inline
    | Unspecified

[<AllowNullLiteral>]
[<Interface>]
type ClipboardResourceItem =
    abstract member ``type``: string with get
    abstract member text: string option with get
    abstract member blob: Blob with get

/// <summary>
/// Returned from the Solid Primitive <c>clipboard</c> library.
/// <br/>
/// Represents the array result of <c>createClipboard</c>. You can skip interacting with
/// this interface by using the apostraphised version of the method and destructuring it
/// using F# language features.
/// </summary>
[<AllowNullLiteral; Interface>]
type ClipboardResult =
    [<Emit("$0[0]")>]
    abstract member resourceItems: SolidResource<ClipboardResourceItem[]> with get
    [<Emit("$0[1]")>]
    abstract member refetch: (unit -> unit) with get
    [<Emit("$0[2]")>]
    abstract member write: (string -> JS.Promise<unit>) with get

[<AllowNullLiteral; Interface>]
type ClipboardItem =
    abstract member presentationStyle: PresentationStyle with get
    abstract member types: string[] with get
    // The getType() method of ClipboardItem interface returns a Promise that resolves with a Blob of the requested MIME type or an error if the MIME type is not found.
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/ClipboardItem/getType">MDN Reference</a>
    /// </summary>
    abstract member getType: ``type``: string -> JS.Promise<Blob>

[<Erase; AutoOpen>]
type Clipboard =
    /// <summary>
    /// newClipboardItem is a wrapper method around creating new ClipboardItems.
    /// </summary>
    /// <param name="type">
    /// The MIME type of the item to create
    /// </param>
    /// <param name="data">
    /// Data to populate the item with
    /// </param>
    /// <returns>
    /// Provides a ClipboardItem object
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member newClipboardItem(``type``: string, data: obj): ClipboardItem = jsNative
    /// <summary>
    /// Async read from the clipboard
    /// </summary>
    /// <returns>
    /// Promise of ClipboardItem array
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member readClipboard (): JS.Promise<ClipboardItem[]> = jsNative

    /// <summary>
    /// Async write to the clipboard.<br/>The apostraphised version of the method will
    /// return the <c>Promise</c>.
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<ImportMember(Spec.path)>]
    static member writeClipboard (data: string): unit = jsNative
    /// <summary>
    /// Async write to the clipboard.<br/>The apostraphised version of the method will
    /// return the <c>Promise</c>.
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<ImportMember(Spec.path)>]
    static member writeClipboard (data: ClipboardItem[]): unit = jsNative
    /// <summary>
    /// Async write to the clipboard. <br/>The unapostraphised version discards the
    /// <c>Promise</c> for cleaner source
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<Import("writeClipboard", Spec.path)>]
    static member writeClipboard'(data: string): JS.Promise<unit> = jsNative
    /// <summary>
    /// Async write to the clipboard. <br/>The unapostraphised version discards the
    /// <c>Promise</c> for cleaner source
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<Import("writeClipboard", Spec.path)>]
    static member writeClipboard'(data: ClipboardItem[]): JS.Promise<unit> = jsNative
    /// <summary>
    /// Creates a new reactive primitive for managing the clipboard.
    /// </summary>
    /// <example>
    /// const [data, setData] = createSignal('Foo bar');
    /// const [ clipboard, read ] = createClipboard(data);
    /// </example>
    /// <param name="data">
    /// Data signal to write to the clipboard.
    /// </param>
    /// <param name="deferInitial">
    /// Sets the value of the clipboard from the signal. defaults to false.
    /// </param>
    /// <returns>
    /// Returns a resource representing the clipboard elements and children.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createClipboard (?data: Accessor<string>, ?deferInitial: bool): ClipboardResult = jsNative
    /// <summary>
    /// Creates a new reactive primitive for managing the clipboard.
    /// </summary>
    /// <example>
    /// const [data, setData] = createSignal('Foo bar');
    /// const [ clipboard, read ] = createClipboard(data);
    /// </example>
    /// <param name="data">
    /// Data signal to write to the clipboard.
    /// </param>
    /// <param name="deferInitial">
    /// Sets the value of the clipboard from the signal. defaults to false.
    /// </param>
    /// <returns>
    /// Returns a resource representing the clipboard elements and children.
    /// </returns>
    static member createClipboard (?data: Accessor<ClipboardItem[]>, ?deferInitial: bool): ClipboardResult = jsNative
    /// <summary>
    /// Same as <c>createClipboard</c> except it returns the result ready for
    /// destructuring in F#
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createClipboard' (?data: Accessor<string>, ?deferInitial: bool): SolidResource<ClipboardResourceItem[]> * (unit -> unit) * (string -> JS.Promise<unit>) = jsNative
    /// <summary>
    /// Same as <c>createClipboard</c> except it returns the result ready for
    /// destructuring in F#
    /// </summary>
    static member createClipboard' (?data: Accessor<ClipboardItem[]>, ?deferInitial: bool): SolidResource<ClipboardResourceItem[]> * (unit -> unit) * (string -> JS.Promise<unit>) = jsNative
    // [<ImportMember(Spec.path)>] // Directive
    // static member copyToClipboard () = jsNative

[<Erase; AutoOpen>]
module private DevicesSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/devices"

[<AllowNullLiteral; Interface>]
type GyroscopeValue =
    abstract member alpha: float with get,set
    abstract member beta: float with get,set
    abstract member gamma: float with get,set

[<Erase; AutoOpen>]
type Devices =
    /// <summary>
    /// Creates a list of all media devices
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the permissions to use the media devices are not granted, you'll get a single device of that kind with empty ids and label to show that devices are available at all.
    ///
    /// If the array does not contain a device of a certain kind, you cannot get permissions, as requesting permissions requires requesting a stream on any device of the kind.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createDevices(): Accessor<MediaDeviceInfo[]> = jsNative
    /// <summary>
    /// Creates a list of all media devices that are microphones
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the microphone permissions are not granted, you'll get a single device with empty ids and label to show that devices are available at all.
    ///
    /// Without a device, you cannot get permissions, as requesting permissions requires requesting a stream on any device of the kind.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createMicrophones(): Accessor<MediaDeviceInfo[]> = jsNative
    /// <summary>
    /// Creates a list of all media devices that are speakers
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the speaker permissions are not granted, you'll get a single device with empty ids and label to show that devices are available at all.
    ///
    /// Microphone permissions automatically include speaker permissions. You can use the device id of the speaker to use the setSinkId-API of any audio tag.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createSpeakers(): Accessor<MediaDeviceInfo[]> = jsNative
    /// <summary>
    /// Creates a list of all media devices that are cameras
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the camera permissions are not granted, you'll get a single device with empty ids and label to show that devices are available at all.
    ///
    /// Without a device, you cannot get permissions, as requesting permissions requires requesting a stream on any device of the kind.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createCameras(): Accessor<MediaDeviceInfo> = jsNative

    /// <summary>
    /// Creates a reactive wrapper to get device acceleration
    /// </summary>
    /// <param name="includeGravity">
    /// boolean. default value false
    /// </param>
    /// <param name="interval">
    /// number as ms. default value 100
    /// </param>
    [<ImportMember(Spec.path)>]
    static member createAccelerometer(?includeGravity: bool, ?interval: float): Accessor<DeviceAcceleration option> = jsNative

    /// <summary>
    /// Creates a reactive wrapper to get device orientation
    /// </summary>
    /// <param name="interval">
    /// number as ms. default value 100
    /// </param>
    [<ImportMember(Spec.path)>]
    static member createGyroscope(?interval: float): Accessor<GyroscopeValue> = jsNative


[<Erase; AutoOpen>]
module private EventBusSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/event-bus"

/// <summary>
/// Provides all the base functions of an event-emitter, plus additional functions for managing listeners, it's behavior could be customized with an config object. Good for advanced usage.
/// </summary>
/// <code>
/// import { createEventBus } from "@solid-primitives/event-bus";
///
/// const bus = createEventBus:string:();
///
/// // can be used without payload type, if you don't want to send any
/// createEventBus();
///
/// // bus can be destructured:
/// const { listen, emit, clear } = bus;
///
/// const unsub = bus.listen(a => console.log(a));
///
/// bus.emit("foo");
///
/// // unsub gets called automatically on cleanup
/// unsub();
/// </code>
[<Erase>]
type EventBus<'T> =
    abstract listen: ('T -> unit) -> DisposeCallback
    abstract emit: 'T -> unit
    abstract clear: unit -> unit
/// <summary>
/// An emitter which you can listen to and emit various events.
/// </summary>
/// <code>
/// import { createEmitter } from "@solid-primitives/event-bus";
///
/// const emitter = createEmitter:{
///   foo: number;
///   bar: string;
/// }:();
/// // can be destructured
/// const { on, emit, clear } = emitter;
///
/// emitter.on("foo", e => {});
/// emitter.on("bar", e => {});
///
/// emitter.emit("foo", 0);
/// emitter.emit("bar", "hello");
///
/// // unsub gets called automatically on cleanup
/// unsub();
/// </code>
[<Erase>]
type Emitter<'MessageTyper> =
    abstract on: (string * (obj -> unit)) -> DisposeCallback
    abstract emit: (string * obj) -> unit
    abstract clear: unit -> unit

/// <summary>
/// Typesafe version of the Emitter made for F#/Fable.
/// It uses the path from the type to its member as the key of the emission. Because we provide a typed path, we
/// get the benefit of having the member being typed as the message type.
/// That is to say, there is nothing stopping you (on the js side) from sending the wrong type through.
/// </summary>
[<Erase>]
type MappedEmitter<'MessageMapper> =
    [<Erase; Emit("$0.on($1, $2)")>]
    member this.zzz_onImplementation (key: string, callback: obj): DisposeCallback = jsNative
    member inline this.on (mapping: 'MessageMapper -> 'MessageType) (callback: 'MessageType -> unit): DisposeCallback =
        this.zzz_onImplementation(JsInterop.emitJsExpr (Experimental.namesofLambda(mapping)) "$0.join('.')", unbox callback)
    [<Erase; Emit("$0.emit($1, $2)")>]
    member this.zzz_emitImplementation(key: string,value: obj): unit = jsNative
    member inline this.emit (mapping: 'MessageMapper -> 'MessageType) (message: 'MessageType): unit =
        this.zzz_emitImplementation(JsInterop.emitJsExpr (Experimental.namesofLambda(mapping)) "$0.join('.')", unbox message)
    [<Erase>]
    member this.clear(): unit = ()

/// <summary>
/// Wrapper around createEmitter.<br/><br/>
/// Creates an emitter with which you can listen to and emit various events. With this emitter you can also listen to all events.
/// </summary>
/// <code>
/// import { createGlobalEmitter } from "@solid-primitives/event-bus";
///
/// const emitter = createGlobalEmitter:{
///   foo: number;
///   bar: string;
/// }:();
/// // can be destructured
/// const { on, emit, clear, listen } = emitter;
///
/// emitter.on("foo", e => {});
/// emitter.on("bar", e => {});
///
/// emitter.emit("foo", 0);
/// emitter.emit("bar", "hello");
///
/// // global listener - listens to all channels
/// emitter.listen(e => {
///   switch (e.name) {
///     case "foo": {
///       e.details;
///       break;
///     }
///     case "bar": {
///       e.details;
///       break;
///     }
///   }
/// });
/// </code>
[<Erase>]
type GlobalEmitter<'T> =
    inherit Emitter<'T>
    abstract listen: (obj -> unit) -> DisposeCallback
/// <summary>
/// Provides helpers for using a group of event buses.<br/><br/>
/// Can be used with createEventBus, createEventStack or any emitter that has the same api.
/// </summary>
/// <code>
/// How to use it
/// /// Creating EventHub
/// import { createEventHub } from "@solid-primitives/event-bus";
///
/// // by passing an record of Channels
/// const hub = createEventHub({
///   busA: createEventBus(),
///   busB: createEventBus:string:(),
///   busC: createEventStack:{ text: string }:(),
/// });
///
/// // by passing a function
/// const hub = createEventHub(bus =@ ({
///   busA: bus:number:(),
///   busB: bus:string:(),
///   busC: createEventStack:{ text: string }:(),
/// }));
///
/// // hub can be destructured
/// const { busA, busB, on, emit, listen, value } = hub;
/// /// Listening  Emitting
/// const hub = createEventHub({
///   busA: createEventBus:void:(),
///   busB: createEventBus:string:(),
///   busC: createEventStack:{ text: string }:(),
/// });
/// // can be destructured
/// const { busA, busB, on, listen, emit } = hub;
///
/// hub.on("busA", e =@ {});
/// hub.on("busB", e =@ {});
///
/// hub.emit("busA", 0);
/// hub.emit("busB", "foo");
///
/// // global listener - listens to all channels
/// hub.listen(e =@ {
///   switch (e.name) {
///     case "busA": {
///       e.details;
///       break;
///     }
///     case "busB": {
///       e.details;
///       break;
///     }
///   }
/// });
/// /// Accessing values
/// // If a emitter returns an accessor value, it will be available in a .value store.
///
/// hub.value.myBus;
/// // same as
/// hub.myBus.value();
/// </code>
[<Erase>]
type EventHub<'T> =
    inherit GlobalEmitter<'T>
//
// [<Erase>]
// type EventStackParameters<'Message> =
//     abstract event: Event
// [<Erase>]
// type EventStackListener<'Message> = EventStackParameters<'Message> -> unit
// [<Erase>]
// type EventStack<'Event, 'PackagedEvent> =
//     inherit GlobalEmitter<'Event>
//
// type EventStackSimple<'Event> = EventStack<'Event, 'Event>

[<Erase; AutoOpen>]
type EventBus =
    [<ImportMember(Spec.path)>]
    static member createEventBus<'T> (): EventBus<'T> = jsNative

    [<ImportMember(Spec.path)>]
    static member createEmitter<'T> (): Emitter<'T> = jsNative
    [<Import("createEmitter",Spec.path)>]
    static member createMappedEmitter<'MessageSchema> (): MappedEmitter<'MessageSchema> = jsNative
    //todo createEventStack & utils
    [<ImportMember(Spec.path)>]
    static member createEventHub<'T> ([<OptionalArgument>] channels: 'T): EventHub<'T> = jsNative


[<Erase; AutoOpen>]
module private EventListenerSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/event-listener"

[<Erase; AutoOpen>]
type EventListener =
    /// <summary>
    /// Creates an event listener, that will be automatically disposed on cleanup.
    /// </summary>
    /// <example>
    /// const clear = makeEventListener(element, 'click', e => { ... }, { passive: true })
    /// // remove listener (will also happen on cleanup)
    /// clear()
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    /// <returns>
    /// Function clearing all event listeners form targets
    /// </returns>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member makeEventListener(
        target: #Element,
        ``type``: string,
        handler: Event -> unit,
        ?capture: bool, ?once: bool, ?passive: bool): DisposeCallback = jsNative
    /// <summary>
    /// Creates an event listener, that will be automatically disposed on cleanup.
    /// </summary>
    /// <example>
    /// const clear = makeEventListener(element, 'click', e => { ... }, { passive: true })
    /// // remove listener (will also happen on cleanup)
    /// clear()
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="options">
    /// addEventListener options
    /// </param>
    /// <returns>
    /// Function clearing all event listeners form targets
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member makeEventListener(
        target: #Element,
        ``type``: string,
        handler: Event -> unit,
        options: AddEventListenerOptions): DisposeCallback = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(
        target: #Element,
        ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>,
        handler: Event -> unit,
        ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(
        target: #Element,
        ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>,
        handler: Event -> unit,
        options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(target: #Element[], ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(target: #Element[], ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(target: Accessor<#Element>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(target: Accessor<#Element>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(target: Accessor<#Element[]>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(target: Accessor<#Element[]>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Provides an reactive signal of last captured event.
    /// </summary>
    /// <example><code>
    /// const lastEvent = createEventSignal(el, 'click', { passive: true })
    ///
    /// createEffect(() => {
    ///    console.log(lastEvent())
    /// })
    /// </code></example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    /// <returns>
    /// Signal of last captured event and function clearing all event listeners
    /// </returns>
    [<ImportMember(Spec.path); System.Obsolete("Unimplemented")>]
    static member createEventSignal([<System.ParamArray>] arguments: obj[]): obj = jsNative


[<Erase>]
module private IdleSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/idle"
        let [<Literal>] version = "0.2.0"

open IdleSpec

[<AllowNullLiteral; Interface>]
type IdleTimer =
    /// Report user status.
    abstract member isIdle: bool Accessor with get
    /// Report user status.
    abstract member isPrompted: bool Accessor with get
    /// Start timer
    abstract member start: unit -> unit with get
    /// Stop timer
    abstract member stop: unit -> unit with get
    /// Reset timer
    abstract member reset: unit -> unit with get
    /// Manually sets isIdle to true and triggers onIdle callback (with custom manualidle event).
    abstract member triggerIdle: unit -> unit with get

[<Erase; AutoOpen>]
type Idle =
    /// Provides different accessors and methods to observe the user's idle status and react to its changing.
    [<ImportMember(Spec.path); ParamObject>]
    static member createIdleTimer (
            ?idleTimeout: int,
            ?promptTimeout: int,
            ?onIdle: (Event -> unit),
            ?onPrompt: (Event -> unit),
            ?onActive: (Event -> unit),
            ?startManually: bool,
            ?events: Event[],
            ?element: HtmlElement
        ): IdleTimer = jsNative

[<Erase>]
module private KeyboardSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/keyboard"
        let [<Literal>] version = ""
open KeyboardSpec.Spec

[<Erase; AutoOpen>]
type Keyboard =
    [<ImportMember(path)>]
    static member useKeyDownEvent (): Accessor<KeyboardEvent> = jsNative

    [<ImportMember(path)>]
    static member useKeyDownList(): Accessor<string[]> = jsNative

    [<ImportMember(path)>]
    static member useCurrentlyHeldKey(): Accessor<string | null> = jsNative

    [<ImportMember(path)>]
    static member useKeyDownSequence(): Accessor<string[][]> = jsNative

    [<ImportMember(path)>]
    static member createKeyHold(key: string): Accessor<bool> = jsNative
    [<ImportMember(path); ParamObject(1)>]
    static member createKeyHold(key: string, preventDefault: bool): Accessor<bool> = jsNative

    [<ImportMember(path)>]
    static member createShortcut(keys: string[], handler: unit -> unit): unit = jsNative
    [<ImportMember(path); ParamObject(2)>]
    static member createShortcut(keys: string[], handler: unit -> unit, ?preventDefault: bool, ?requireReset: bool ): unit = jsNative


[<Erase>]
module private MediaSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/media"
        let [<Literal>] version = ""

open MediaSpec.Spec

[<Erase>]
type BreakpointMonitor<'T> =
    member inline this.obj = unbox<'T> this
    member _.key with get(): string = jsNative

[<Erase>]
type MediaQuery = (unit -> bool)

[<Interface; AllowNullLiteral>]
type MediaQueryEvent =
    abstract matches: bool
    abstract media: string

[<Erase; AutoOpen>]
type Media =
    [<ImportMember(path)>]
    static member makeMediaQueryListener(query: string) (handler: MediaQueryEvent -> unit): DisposeCallback = jsNative
    [<ImportMember(path)>]
    static member createMediaQuery (query: string, ?serverFallback: bool): MediaQuery = jsNative
    [<ImportMember(path)>]
    static member createBreakpoints (queryMonitor: 'T): BreakpointMonitor<'T> = jsNative
    [<ImportMember(path)>]
    static member sortBreakpoints (breakpoints: 'T): 'T = jsNative
    [<ImportMember(path)>]
    static member createPrefersDark (?fallback: bool): MediaQuery = jsNative
    [<ImportMember(path)>]
    static member usePrefersDark (): MediaQuery = jsNative


[<Erase; AutoOpen>]
module private MouseSpec =
    [<Erase>]
    module Spec =
        let [<Erase; Literal>] path = "@solid-primitives/mouse"
        let [<Erase; Literal>] version = "2.1.2"

open Spec

[<StringEnum>]
type MouseSourceType =
    | Mouse
    | Touch

[<JS.Pojo>]
type Position(?x:int,?y:int) =
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set

[<JS.Pojo>]
type MousePosition(
        ?x: int
        ,?y: int
        ,?sourceType: MouseSourceType
    ) =
    inherit Position(?x=x,?y=y)
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set
    /// Can be null
    member val sourceType: MouseSourceType option = sourceType with get,set

[<JS.Pojo>]
type MousePositionInside(
        ?x: int
        ,?y: int
        ,?isInside: bool
        ,?sourceType: MouseSourceType
    ) =
    inherit MousePosition(?x=x, ?y=y, ?sourceType = sourceType)
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set
    member val isInside: bool = JS.undefined with get,set
    /// Can be null
    member val sourceType: MouseSourceType option = sourceType with get,set

[<JS.Pojo>]
type PositionRelativeToElement(
        ?x: int
        ,?y: int
        ,?top: int
        ,?left: int
        ,?width: int
        ,?height: int
        ,?isInside: bool
    ) =
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set
    member val top: int = JS.undefined with get,set
    member val left: int = JS.undefined with get,set
    member val width: int = JS.undefined with get,set
    member val height: int = JS.undefined with get,set
    member val isInside: bool = JS.undefined with get,set

[<Erase; AutoOpen>]
type Mouse =
    /// <summary>
    /// Attaches event listeners to provided targat, listeneing for changes to the mouse/touch position.
    /// </summary>
    /// <param name="target">
    /// <code lang="ts">
    /// SVGSVGElement | HTMLElement | Window | Document
    /// </code>
    /// </param>
    /// <param name="callback">
    /// function fired on every position change
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// function removing all event listeners
    /// </returns>
    /// <seealso cref="UseTouchOptions">UseTouchOptions</seealso>
    /// <seealso cref="FollowTouchOptions">FollowTouchOptions</seealso>
    [<Import("makeMousePositionListener", path); ParamObject(2)>]
    static member makeMousePositionListener (target: U4<HtmlElement, Element, Document, Window>, callback: (MousePosition -> unit), ?touch: bool, ?followTouch: bool) : DisposeCallback = nativeOnly
    /// <summary>
    /// Attaches event listeners to provided targat, listening for mouse/touch entering/leaving the element.
    /// </summary>
    /// <param name="target">
    /// <code lang="ts">
    /// SVGSVGElement | HTMLElement | Window | Document
    /// </code>
    /// </param>
    /// <param name="callback">
    /// function fired on mouse leaving or entering the element
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <returns>
    /// function removing all event listeners
    /// </returns>
    [<Import("makeMouseInsideListener", path); ParamObject(2)>]
    static member makeMouseInsideListener (target: U4<HtmlElement, Element, Document, Window>, callback: (bool -> unit), ?touch: bool) : DisposeCallback = nativeOnly
    /// <summary>
    /// Turn position relative to the page, into position relative to an element.
    /// </summary>
    [<Import("getPositionToElement", path)>]
    static member getPositionToElement(pageX: int, pageY: int, el: U4<HtmlElement, Element, Document, Window>): PositionRelativeToElement = nativeOnly
    /// <summary>
    /// Turn position relative to the page, into position relative to an element. Clamped to the element bounds.
    /// </summary>
    [<Import("getPositionInElement", path)>]
    static member getPositionInElement(pageX: int, pageY: int, el: U4<HtmlElement, Element, Document, Window>): PositionRelativeToElement = nativeOnly
    /// <summary>
    /// Turn position relative to the page, into position relative to the screen.
    /// </summary>
    [<Import("getPositionToScreen", path)>]
    static member getPositionToScreen(pageX: int, pageY: int): Position = nativeOnly
    /// <summary>
    /// Attaches event listeners to <see href="target">target</see> element to provide a reactive object of current mouse position on the page.
    /// </summary>
    /// <example>
    /// const [el, setEl] = createSignal(ref)
    /// const pos = createMousePosition(el, { touch: false })
    /// createEffect(() => {
    ///   console.log(pos.x, pos.y)
    /// })
    /// </example>
    /// <param name="target">
    /// (Defaults to <c>window</c>) element to attach the listeners to – can be a reactive function
    /// </param>
    /// <param name="initialValues">
    /// Initial values
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// reactive object of current mouse position on the page
    /// <code lang="ts">
    /// { x: number, y: number, sourceType: MouseSourceType, isInside: boolean }
    /// </code>
    /// </returns>
    [<Import("createMousePosition", path)>]
    static member createMousePosition (?target: U4<HtmlElement, Element, Document, Window>, ?initialValues: MousePositionInside, ?touch: bool, ?followTouch: bool) : MousePositionInside = nativeOnly
    /// <summary>
    /// Attaches event listeners to <see href="target">target</see> element to provide a reactive object of current mouse position on the page.
    /// </summary>
    /// <example>
    /// const [el, setEl] = createSignal(ref)
    /// const pos = createMousePosition(el, { touch: false })
    /// createEffect(() => {
    ///   console.log(pos.x, pos.y)
    /// })
    /// </example>
    /// <param name="target">
    /// (Defaults to <c>window</c>) element to attach the listeners to – can be a reactive function
    /// </param>
    /// <param name="initialValues">
    /// Initial values
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// reactive object of current mouse position on the page
    /// <code lang="ts">
    /// { x: number, y: number, sourceType: MouseSourceType, isInside: boolean }
    /// </code>
    /// </returns>
    [<Import("createMousePosition", path); ParamObject(1)>]
    static member createMousePosition (?target: U4<Accessor<HtmlElement>, Accessor<Element>, Accessor<Document>, Accessor<Window>>, ?initialValues: MousePositionInside, ?touch: bool, ?followTouch: bool) : MousePositionInside = nativeOnly
    /// <summary>
    /// Attaches event listeners to <c>window</c> to provide a reactive object of current mouse position on the page.
    ///
    /// This is a [singleton root primitive](https://github.com/solidjs-community/solid-primitives/tree/main/packages/rootless#createSingletonRoot).
    /// </summary>
    /// <example>
    /// const pos = useMousePosition()
    /// createEffect(() => {
    ///   console.log(pos.x, pos.y)
    /// })
    /// </example>
    /// <returns>
    /// reactive object of current mouse position on the page
    /// <code lang="ts">
    /// { x: number, y: number, sourceType: MouseSourceType, isInside: boolean }
    /// </code>
    /// </returns>
    [<Import("useMousePosition", path)>]
    static member inline useMousePosition: (unit -> MousePositionInside) = nativeOnly
    /// <summary>
    /// Provides an autoupdating position relative to an element based on provided page position.
    /// </summary>
    /// <example>
    /// const [el, setEl] = createSignal(ref)
    /// const pos = useMousePosition()
    /// const relative = createPositionToElement(el, () => pos)
    /// createEffect(() => {
    ///   console.log(relative.x, relative.y)
    /// })
    /// </example>
    /// <param name="element">
    /// target <c>Element</c> used in calculations
    /// </param>
    /// <param name="pos">
    /// reactive function returning page position *(relative to the page not window)*
    /// </param>
    /// <param name="initialValues">
    /// Initial values
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// Autoupdating position relative to top-left of the target + current bounds of the element.
    /// </returns>
    [<Import("createPositionToElement", path); ParamObject(2)>]
    static member createPositionToElement (element: U4<HtmlElement, Element, Accessor<HtmlElement>, Accessor<Element>>, pos: Accessor<Position>, ?initialValues: PositionRelativeToElement, ?touch: bool, ?followTouch: bool) : PositionRelativeToElement = nativeOnly


[<Erase; AutoOpen>]
module private RafSpec =
    [<Erase>]
    module Spec =
        let [<Erase; Literal>] path = "@solid-primitives/raf"
        let [<Erase; Literal>] version = "2.3.1"

open Spec

type FrameRequestCallback = float -> unit
[<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
type StartVoidFunction = (unit -> unit)
[<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
type StopVoidFunction = (unit -> unit)

[<AllowNullLiteral>]
[<Interface>]
type MsCounter =
    [<Emit("$0()")>]
    abstract member current: int with get
    abstract member reset: (unit -> unit) with get
    abstract member running: (unit -> bool) with get
    abstract member start: (unit -> unit) with get
    abstract member stop: (unit -> unit) with get


[<Erase; AutoOpen>]
type Raf =
    /// <summary>
    /// A primitive creating reactive <c>window.requestAnimationFrame</c>, that is automatically disposed onCleanup.
    /// </summary>
    /// <example><code>
    /// const [running, start, stop] = createRAF((timestamp) => {
    ///    el.style.transform = "translateX(...)"
    /// });
    /// </code></example>
    /// <param name="callback">
    /// The callback to run each frame
    /// </param>
    /// <returns>
    /// Returns a signal if currently running as well as start and stop methods
    /// <code lang="ts">
    /// [running: Accessor&lt;boolean>, start: VoidFunction, stop: VoidFunction]
    /// </code>
    /// </returns>
    [<Import("createRAF", path)>]
    static member createRAF (callback: FrameRequestCallback) : Accessor<bool> * StartVoidFunction * StopVoidFunction = nativeOnly
    /// <summary>
    /// A primitive for wrapping <c>window.requestAnimationFrame</c> callback function to limit the execution of the callback to specified number of FPS.
    ///
    /// Keep in mind that limiting FPS is achieved by not executing a callback if the frames are above defined limit. This can lead to not consistant frame duration.
    /// </summary>
    /// <example><code>
    /// const [running, start, stop] = createRAF(
    ///   targetFPS(() => {...}, 60)
    /// );
    /// </code></example>
    /// <param name="callback">
    /// The callback to run each *allowed* frame
    /// </param>
    /// <param name="fps">
    /// The target FPS limit
    /// </param>
    /// <returns>
    /// Wrapped RAF callback
    /// </returns>
    [<Import("targetFPS", path)>]
    static member targetFPS (callback: FrameRequestCallback, fps: float) : FrameRequestCallback = nativeOnly
    /// <summary>
    /// A primitive for wrapping <c>window.requestAnimationFrame</c> callback function to limit the execution of the callback to specified number of FPS.
    ///
    /// Keep in mind that limiting FPS is achieved by not executing a callback if the frames are above defined limit. This can lead to not consistant frame duration.
    /// </summary>
    /// <example><code>
    /// const [running, start, stop] = createRAF(
    ///   targetFPS(() => {...}, 60)
    /// );
    /// </code></example>
    /// <param name="callback">
    /// The callback to run each *allowed* frame
    /// </param>
    /// <param name="fps">
    /// The target FPS limit
    /// </param>
    /// <returns>
    /// Wrapped RAF callback
    /// </returns>
    [<Import("targetFPS", path)>]
    static member targetFPS (callback: FrameRequestCallback, fps: Accessor<float>) : FrameRequestCallback = nativeOnly
    /// <summary>
    /// A primitive that creates a signal counting up milliseconds with a given frame rate to base your animations on.
    /// </summary>
    /// <param name="fps">
    /// the frame rate, either as Accessor or number
    /// </param>
    /// <param name="limit">
    /// an optional limit, either as Accessor or number, after which the counter is reset
    /// </param>
    /// <returns>
    /// an Accessor returning the current number of milliseconds and the following methods:
    /// - <c>reset()</c>: manually resetting the counter
    /// - <c>running()</c>: returns if the counter is currently setRunning
    /// - <c>start()</c>: restarts the counter if stopped
    /// - <c>stop()</c>: stops the counter if running
    ///
    /// <code lang="ts">
    /// const ms = createMs(60);
    /// createEffect(() => ms() > 500000 ? ms.stop());
    /// return &lt;rect x="0" y="0" height="10" width={Math.min(100, ms() / 5000)} />
    /// </code>
    /// </returns>
    /// <remarks>
    /// Contrary to the original implementation, the binding accesses the current value using <c>ms.current</c> instead of <c>ms()</c>
    /// </remarks>
    [<Import("createMs", path)>]
    static member createMs (fps: U4<float, Accessor<float>, int, Accessor<int>>, ?limit: U4<float, Accessor<float>, int, Accessor<int>>): MsCounter = nativeOnly


[<Erase; AutoOpen>]
module private ScheduledSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/scheduled"
        let [<Literal>] version = "1.5.0"

open Spec


[<AllowNullLiteral; Interface>]
type Schedule<'T> =
    [<Emit("$0($1)")>]
    abstract member exec: 'T -> unit
    abstract member clear: unit -> unit with get

type DebounceOrThrottle<'T> = ('T -> unit) * int -> Schedule<'T>

[<Erase>]
type Scheduled =
    [<ImportMember(path)>]
    static member debounce (callback: 'T -> unit, timespan: int): Schedule<'T> = jsNative
    [<ImportMember(path)>]
    static member throttle (callback: 'T -> unit, timespan: int): Schedule<'T> = jsNative
    [<ImportMember(path)>]
    static member scheduleIdle (callback: 'T -> unit, timespan: int): Schedule<'T> = jsNative
    [<ImportMember(path)>]
    static member leading(debOrThrot: DebounceOrThrottle<'T>, callback: 'T -> unit, timespan: int): Schedule<'T> = jsNative
    [<ImportMember(path)>]
    static member leadingAndTrailing (debOrThrot: DebounceOrThrottle<'T>, callback: 'T -> unit, timespan: int): Schedule<'T> = jsNative
    // [<ImportMember(path)>]
    // static member createScheduled (schedule: ('T -> unit) -> ()

[<Erase; AutoOpen>]
module private ScrollSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/scroll"
        let [<Literal>] version = "2.1.0"

open Spec

[<AllowNullLiteral; Interface>]
type ScrollPosition =
    abstract member x: int with get
    abstract member y: int with get

[<Erase>]
type Scroll =
    /// Default target of window
    [<ImportMember(path)>]
    static member createScrollPosition (): Accessor<ScrollPosition> = jsNative
    /// Element ref target or Accessor<#HtmlElement>
    [<ImportMember(path)>]
    static member createScrollPosition (element: unit -> #HtmlElement): Accessor<ScrollPosition> = jsNative
    /// Returns reactive object with current window scroll position; signals and event-listeners are shared
    /// between dependendents making it more optimised to use in multiple places at once
    [<ImportMember(path)>]
    static member useWindowScrollPosition (): ScrollPosition = jsNative
    /// <summary>
    /// Gets a <c>ScrollPosition</c> element/window scroll position
    /// </summary>
    [<ImportMember(path)>]
    static member getScrollPosition (): ScrollPosition = jsNative

[<Erase; AutoOpen>]
module private SpringSpec =
    [<Erase>]
    module Spec =
        let [<Erase; Literal>] path = "@solid-primitives/spring"
        let [<Erase; Literal>] version = "0.1.1"

open Spec

[<JS.Pojo>]
type SpringSetterOptions(?hard: bool, ?soft: U2<bool, float>) =
    member val hard = hard with get,set
    member val soft = soft with get,set

module SetterExtensions =
    [<AutoOpen; Erase>]
    type SetterExtensions =
        [<System.Runtime.CompilerServices.Extension>]
        static member inline Invoke(setter: float -> unit, newValue: float, options: SpringSetterOptions) = unbox(newValue, options) |> setter

[<Erase; AutoOpen>]
type Spring =
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: float, ?stiffness: float, ?damping: float): Signal<float> = jsNative
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: int, ?stiffness: float, ?damping: float): Signal<int> = jsNative
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: float seq, ?stiffness: float, ?damping: float): Signal<float seq> = jsNative
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: int seq, ?stiffness: float, ?damping: float): Signal<int seq> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<int>, ?stiffness: float, ?damping: float): Accessor<int> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<float>, ?stiffness: float, ?damping: float): Accessor<float> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<int seq>, ?stiffness: float, ?damping: float): Accessor<int seq> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<float seq>, ?stiffness: float, ?damping: float): Accessor<float seq> = jsNative

[<Erase; AutoOpen>]
module private TimerSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/timer"
        let [<Literal>] version = "1.4.0"

open Spec

[<Erase>]
type IntervalOrTimeout = (unit -> unit) -> int -> int

[<Erase; AutoOpen>]
type Timer =
    /// Makes an automatically cleaned up timer. Takes a callback, the timespan, and then either the
    /// the function `setInterval` or `setTimeout`
    [<ImportMember(path)>]
    static member makeTimer (callback: unit -> unit, timespan: int, policy: IntervalOrTimeout): DisposeCallback = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: int, policy: IntervalOrTimeout): unit = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: unit -> U2<bool, int>, policy: IntervalOrTimeout): unit = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: unit -> int, policy: IntervalOrTimeout): unit = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: unit -> bool, policy: IntervalOrTimeout): unit = jsNative
    /// <summary>
    /// Similar to an interval created with createTimer, but the delay does not update until the callback is executed
    /// </summary>
    /// <example><code>
    /// let cb = fun () -> ()
    /// let delay,setDelay = createSignal(1000)
    /// createTimeoutLoop(cb, delay)
    /// //...
    /// 500 |> setDelay
    /// </code></example>
    [<ImportMember(path)>]
    static member createTimeoutLoop (callback: unit -> unit, timespan: int): unit = jsNative
    [<ImportMember(path)>]
    static member createTimeoutLoop (callback: unit -> unit, timespan: Accessor<int>): unit = jsNative
    /// <summary>
    /// Periodically polls a function, returning an accessor to its last return value.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createPolled(callback: unit -> 'T, timespan: int): Accessor<'T> = jsNative
    /// <summary>
    /// Periodically polls a function, returning an accessor to its last return value.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createPolled(callback: unit -> 'T, timespan: Accessor<int>): Accessor<'T> = jsNative
    /// <summary>
    /// A counter which increments periodically on the delay.
    /// </summary>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createIntervalCounter(timespan: int): Accessor<int> = jsNative
    /// <summary>
    /// A counter which increments periodically on the delay.
    /// </summary>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createIntervalCounter(timespan: Accessor<int>): Accessor<int> = jsNative

[<Erase; AutoOpen>]
module private TriggerSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/trigger"

open Spec

type [<Erase>] Track<'T> = 'T -> unit
type [<Erase>] Dirty<'T> = 'T -> unit
type [<Erase>] DirtyAll = unit -> unit
type [<Erase>] TriggerSignal<'T> = Track<'T> * Dirty<'T>
type [<Erase>] TriggerCacheSignal<'T> = Track<'T> * Dirty<'T> * DirtyAll

[<AutoOpen; Erase>]
type Extensions =
    /// <summary>
    /// Will track the trigger of the given key for the cache
    /// </summary>
    /// <param name="triggerCache"></param>
    /// <param name="key">the key</param>
    [<Erase; Extension>]
    static member track (triggerCache: TriggerCacheSignal<'T>, key: 'T): unit = undefined
    /// <summary>
    /// Will trigger the tracker for the given key of the cache
    /// </summary>
    /// <param name="triggerCache"></param>
    /// <param name="key">the key</param>
    [<Erase; Extension>]
    static member dirty (triggerCache: TriggerCacheSignal<'T>, key: 'T): unit = undefined

[<Erase; AutoOpen>]
type Trigger =
    /// <summary>
    /// Set listeners in reactive computations and then trigger them when you want.
    /// </summary>
    /// <example><code>
    /// let track,dirty = createTrigger()
    /// createEffect(fun() ->
    ///     track() // 'read' track
    ///     // ...
    /// )
    /// // later
    /// dirty()
    /// </code></example>
    [<ImportMember(path)>]
    static member createTrigger (): TriggerSignal<unit> = jsNative
    /// <summary>
    /// Creates a cache of triggers that can be used to mark dirty only specific keys.
    /// <br/><br/>Cache is a Map or WeakMap depending on the mapConstructor argument. (default: Map)
    /// <br/><br/>If mapConstructor is WeakMap then the cache will be weak and the keys will be garbage collected when they are no longer referenced.
    /// <br/><br/>Trigger signals added to the cache only when tracked under a computation, and get deleted from the cache when they are no longer tracked.
    /// </summary>
    /// <example><code>
    /// let map = createTriggerCache[int]()
    /// createEffect(fun() ->
    ///     map.track(1) // 'read' track
    ///     // ...
    /// )
    /// // later
    /// map.dirty(1)
    /// </code></example>
    [<ImportMember(path)>]
    static member createTriggerCache<'T> (): TriggerCacheSignal<'T> = jsNative

[<Erase; AutoOpen>]
module private TweenSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/tween"
        let [<Literal>] version = "1.4.0"

open Spec

/// <summary>
/// Suggested to use easing functions that are already prepared:
/// <code lang="fsharp">
/// let tweenedValue = createTween(value, CreateTweenOptions(500, Easing.easeInSine))
/// </code>
/// </summary>
type Easing = (float -> float)
/// Contains typical easing functions
[<Erase; AutoOpen>]
module Easing =
    let easeInSine: Easing = fun x -> 1. - Math.cos((x * Math.PI) / 2.)
    let easeOutSine: Easing = fun x  -> Math.sin((x * Math.PI) / 2.)
    let easeInOutSine: Easing = fun x -> -(Math.cos(Math.PI  * x) - 1.) / 2.
    let easeInQuad: Easing = fun x -> x * x
    let easeOutQuad: Easing = fun x -> 1. - (1. - x) * (1. - x)
    let easeInOutQuad: Easing = fun x -> if x < 0.5 then 2. * x * x else 1. - (-2. * x + 2.) * (-2. * x + 2.) / 2.
    let easeInCubic: Easing = fun x -> x * x * x
    let easeOutCubic: Easing = fun x -> 1. - (1. - x) * (1. - x) * (1. - x)
    let easeInOutCubic: Easing = fun x -> if x < 0.5 then 4. * x * x * x else 1. - (-2. * x + 2.) ** 3. / 2.
    let easeInQuart: Easing = fun x -> x * x * x * x
    let easeOutQuart: Easing = fun x -> 1. - (1. - x) ** 4.
    let easeInOutQuart: Easing = fun x -> if x < 0.5 then 8. * x * x * x * x else 1. - (-2. * x + 2.) ** 4. / 2.
    let easeInQuint: Easing = fun x -> x * x * x * x * x
    let easeOutQuint: Easing = fun x -> 1. - (1. - x) ** 5.
    let easeInOutQuint: Easing = fun x -> if x < 0.5 then 16. * x * x * x * x * x else 1. - (-2. * x + 2.) ** 5. / 2.
    let easeInExpo: Easing = fun x -> if x = 0. then 0. else 2. ** (10. * x - 10.)
    let easeOutExpo: Easing = fun x -> if x = 1. then 1. else 1. - 2. ** (-10. * x)
    let easeInOutExpo: Easing = fun x ->
        if x = 0. then 0.
        elif x = 1. then 1.
        else if x < 0.5 then 2. ** (20. * x - 10.) / 2. else (2. - 2. ** (-20. * x + 10.)) / 2.
    let easeInCirc: Easing = fun x -> 1. - Math.sqrt(1. - x * x)
    let easeOutCirc: Easing = fun x -> Math.sqrt(1. - (x - 1.) * (x - 1.))
    let easeInOutCirc: Easing = fun x ->
        if x < 0.5 then (1. - Math.sqrt(1. - (2. * x) ** 2.)) / 2. else (Math.sqrt(1. - (-2. * x + 2.) ** 2.) + 1.) / 2.

    let easeInBack: Easing = fun x -> let c1 = 1.70158 in c1 * x * x * x - c1 * x * x
    let easeOutBack: Easing = fun x -> let c1 = 1.70158 in 1. + c1 * (x - 1.) * (x - 1.) * (x - 1.) + c1 * (x - 1.) * (x - 1.)
    let easeInOutBack: Easing = fun x ->
        let c2 = 2.5949095
        if x < 0.5 then (c2 * (2. * x) * (2. * x) * (2. * x) - c2 * (2. * x) * (2. * x)) / 2.
        else (c2 * (-2. * x + 2.) * (-2. * x + 2.) * (-2. * x + 2.) + c2 * (-2. * x + 2.) * (-2. * x + 2.) + 2.) / 2.

    let easeInElastic: Easing = fun x ->
        if x = 0. then 0.
        elif x = 1. then 1.
        else -2. ** (10. * x - 10.) * Math.sin((x * 10. - 10.75) * (2. * Math.PI) / 3.)

    let easeOutElastic: Easing = fun x ->
        if x = 0. then 0.
        elif x = 1. then 1.
        else 2. ** (-10. * x) * Math.sin((x * 10. - 0.75) * (2. * Math.PI) / 3.) + 1.

    let easeInOutElastic: Easing = fun x ->
        if x = 0. then 0.
        elif x = 1. then 1.
        else if x < 0.5 then (-2. ** (20. * x - 10.) * Math.sin((20. * x - 11.125) * (2. * Math.PI) / 4.5)) / 2.
        else (2. ** (-20. * x + 10.) * Math.sin((20. * x - 11.125) * (2. * Math.PI) / 4.5)) / 2. + 1.

    let easeOutBounce: Easing = fun x ->
        if x < 1. / 2.75 then 7.5625 * x * x
        elif x < 2. / 2.75 then 7.5625 * (x - 1.5 / 2.75) * (x - 1.5 / 2.75) + 0.75
        elif x < 2.5 / 2.75 then 7.5625 * (x - 2.25 / 2.75) * (x - 2.25 / 2.75) + 0.9375
        else 7.5625 * (x - 2.625 / 2.75) * (x - 2.625 / 2.75) + 0.984375
    let easeInBounce: Easing = fun x -> 1. - easeOutBounce(1. - x)

    let easeInOutBounce: Easing = fun x ->
        if x < 0.5 then (1. - easeOutBounce(1. - 2. * x)) / 2.
        else (1. + easeOutBounce(2. * x - 1.)) / 2.
    let easeLinear:  Easing = id

[<Erase; AutoOpen>]
type Tween =
    /// <summary>
    /// Creates an efficient tweening derived signal that smoothly transitions a given signal from its previous value to its next value whenever it changes.
    /// <br/>
    /// target can be any reactive value (signal, memo, or function that calls such). For example, to use a component prop, specify <c>fun () -> props.value</c>.<br/><br/>
    /// You can provide two options:
    /// <br/>
    /// - duration is the number of milliseconds to perform the transition from the previous value to the next value. Defaults to 100.
    /// <br/>- easing is a function that maps a number between 0 and 1 to a number between 0 and 1, to speed up or slow down different parts of the transition. The default easing function (t) => t is linear (no easing). A common choice is (t) => 0.5 - Math.cos(Math.PI * t) / 2.
    /// <br/><br/>Internally, createTween uses requestAnimationFrame to smoothly update the tweened value at the display refresh rate. After the tweened value reaches the underlying signal value, it will stop updating via requestAnimationFrame for efficiency.
    /// </summary>
    [<ImportMember(path); ParamObject(1)>]
    static member createTween(target: Accessor<'T>, ?duration: int, ?easing: Easing): Accessor<'T> = jsNative
    /// <summary>
    /// Creates an efficient tweening derived signal that smoothly transitions a given signal from its previous value to its next value whenever it changes.
    /// <br/>
    /// target can be any reactive value (signal, memo, or function that calls such). For example, to use a component prop, specify <c>fun () -> props.value</c>.<br/><br/>
    /// You can provide two options:
    /// <br/>
    /// - duration is the number of milliseconds to perform the transition from the previous value to the next value. Defaults to 100.
    /// <br/>- easing is a function that maps a number between 0 and 1 to a number between 0 and 1, to speed up or slow down different parts of the transition. The default easing function (t) => t is linear (no easing). A common choice is (t) => 0.5 - Math.cos(Math.PI * t) / 2.
    /// <br/><br/>Internally, createTween uses requestAnimationFrame to smoothly update the tweened value at the display refresh rate. After the tweened value reaches the underlying signal value, it will stop updating via requestAnimationFrame for efficiency.
    /// </summary>
    [<ImportMember(path)>]
    static member createTween(target: Accessor<'T>): Accessor<'T> = jsNative

